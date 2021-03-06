using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class TubeMovement : MonoBehaviour
{
    public static event System.Action OnAnyTubeStartBecomeActive;
    public static event System.Action OnAnyTubeStartGiving;

    public static bool IsTransfering { get; private set; }

    public static Tube ActiveTube { get; private set; }

    [SerializeField]
    private Tube tube;
    [SerializeField]
    private TubeView tubeView;
    [SerializeField]
    private Transform glass;

    [Space]
    [SerializeField]
    private float move_Speed;
    [SerializeField]
    private float rotation_Speed;
    [SerializeField]
    private float water_Speed;
    [SerializeField]
    private float maxRotation;
    [SerializeField]
    private float liftUpFactor = 0.6f;
    [SerializeField]
    private float givingLiftUpFactor = 0.6f;

    [Space]
    [SerializeField]
    private WaterWave waterWave;
    [SerializeField]
    private SortingGroup sortingGroup;

    [Space]
    public Collider2D fixedWaterCollider;
    public Collider2D waterSqureCollider;

    [Space]
    [SerializeField]
    private UnityEvent onStartedGivingWater;
    [SerializeField]
    private UnityEvent onFinishedGivingWater;
    [SerializeField]
    private UnityEvent onStartedTransferringWater;
    [SerializeField]
    private UnityEvent onMoveAwayFromGround;
    [SerializeField]
    private UnityEvent onBackToGround;
    [SerializeField]
    private UnityEvent onIncorrect;
    [SerializeField]
    private UnityEvent onCompleted;

    [Space]
    [SerializeField]
    private UnityEvent startShakingDelegate;
    [SerializeField]
    private UnityEvent stopShakingDelegate;

    private float initialX;
    private float initialY;

    private bool mouseDownThisFrame = false;

    private List<Tube> givingTubes = new List<Tube>();
    private Tube receiving_Tube;
    private bool transfering_Water = false;
    private bool finishedTransfering = false;
    private bool particleHit = false;

    private float ActiveAdditionalY => TubeView.MaxWorldGlassHeight * liftUpFactor;
    private float GivingAdditionalY => TubeView.MaxWorldGlassHeight * givingLiftUpFactor;

    public bool IsCompleted { get; private set; } = false;

    public TubeMovementState State { get; private set; } = TubeMovementState.Idle;

    public void Awake()
    {
        ///
        fixedWaterCollider.enabled = false;
        waterSqureCollider.enabled = false;

        ///
        EntryInGame.Instance.gameStateManager.OnStateChanged += GameStateManager_OnStateChanged;
        EntryInGame.Instance.gameStateManager.OnReset += GameStateManager_OnReset;
    }

    private void GameStateManager_OnReset()
    {
        IsTransfering = false;
    }

    private void GameStateManager_OnStateChanged()
    {
        ///
        if (!gameObject.activeSelf)
        {
            return;
        }

        ///
        var gameStateMan = EntryInGame.Instance.gameStateManager;
        if (gameStateMan.CurrentState == GameState.Over && gameStateMan.Won_Flag)
        {
            if (State == TubeMovementState.Active)
            {
                StartCoroutine(BecomeIdle());
            }
            else if (State == TubeMovementState.BecomingActive)
            {
                StopAllCoroutines();
                StartCoroutine(BecomeIdle());
            }
        }
    }

    public void OnDestroy()
    {
        EntryInGame.Instance.gameStateManager.OnReset -= GameStateManager_OnReset;
        EntryInGame.Instance.gameStateManager.OnStateChanged -= GameStateManager_OnStateChanged;
    }

    public void SaveInitialPosition()
    {
        var p = transform.localPosition;
        initialX = p.x;
        initialY = p.y;
    }

    public void SaveInitialX()
    {
        var p = transform.localPosition;
        initialX = p.x;
    }

    public void OnMouseDown()
    {
        mouseDownThisFrame = true;
    }

    public void LateUpdate()
    {
        ///
        if (mouseDownThisFrame && UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == null)
        {
            HandleMouseDown();
        }

        ///
        mouseDownThisFrame = false;
    }

    private void HandleMouseDown()
    {
        ///
        if (IsTransfering)
        {
            return;
        }

        ///
        if (IsCompleted)
        {
            return;
        }

        ///
        if (EntryInGame.Instance.gameStateManager.CurrentState == GameState.Over)
        {
            return;
        }

        ///
        if (State == TubeMovementState.Idle)
        {
            if (ActiveTube != null)
            {
                if (!TryReceivingFromActiveTube())
                {
                    ActiveTube.tubeMovement.StartCoroutine(ActiveTube.tubeMovement.BecomeIdle());
                    StartCoroutine(BecomeActive());
                }
            }
            else
            {
                ///
                if (tube.CurrentWaterHeight > 0)
                {
                    StartCoroutine(BecomeActive());
                }
                else
                {
                    Shake();
                }

                ///
                if (EntryInGame.Instance.gameStateManager.CurrentState == GameState.Prepare)
                {
                    EntryInGame.Instance.gameStateManager.SwitchToBeat();
                }
            }
        }
        else if (State == TubeMovementState.Active)
        {
            onIncorrect?.Invoke();
            StartCoroutine(BecomeIdle());
        }
        else if (State == TubeMovementState.Receiving)
        {
            //if (activeTube != null)
            //{
            //    TryReceivingFromActiveTube();
            //}
        }
    }

    private IEnumerator BecomeActive()
    {
        ///
        OnAnyTubeStartBecomeActive?.Invoke();

        ///
        sortingGroup.sortingOrder = 1;

        ///
        StopShaking();

        ///
        ActiveTube = this.tube;
        State = TubeMovementState.BecomingActive;

        ///
        onMoveAwayFromGround?.Invoke();

        ///
        float y = initialY;
        float targetY = initialY + ActiveAdditionalY;

        ///
        while (y < targetY)
        {
            ///
            y = Mathf.MoveTowards(y, targetY, move_Speed * Time.deltaTime);

            ///
            var p = transform.localPosition;
            p.y = y;
            transform.localPosition = p;

            ///
            yield return null;
        }
        
        ///
        State = TubeMovementState.Active;
    }

    private IEnumerator BecomeIdle()
    {
        ///
        StopShaking();

        ///
        if (ActiveTube == tube)
        {
            ActiveTube = null;
        }
        State = TubeMovementState.BecomingIdle;

        ///
        float angle = glass.rotation.eulerAngles.z;
        float targetAngle = 0;
        while (!Mathf.Approximately(angle, targetAngle))
        {
            ///
            angle = Mathf.MoveTowardsAngle(angle, targetAngle, rotation_Speed * Time.deltaTime);
            glass.rotation = Quaternion.Euler(0, 0, angle);

            ///
            yield return null;
        }

        ///
        Vector2 pos = transform.localPosition;
        Vector2 targetPos = new Vector2(initialX, initialY);

        ///
        while (!Mathf.Approximately(pos.x, targetPos.x) || !Mathf.Approximately(pos.y, targetPos.y))
        {
            ///            
            pos = Vector2.MoveTowards(pos, targetPos, move_Speed * Time.deltaTime);

            ///
            transform.localPosition = pos;

            ///
            yield return null;
        }

        ///
        State = TubeMovementState.Idle;

        ///
        sortingGroup.sortingOrder = 0;
        
        ///
        onBackToGround?.Invoke();
    }

    private bool TryReceivingFromActiveTube()
    {
        ///
        if (tube.CurrentWaterHeight >= tube.GlassHeight)
        {
            // activeTube?.tubeMovement.Shake();
            return false;
        }

        ///
        if (ActiveTube.TopColorId != tube.TopColorId && tube.TopColorId >= 0)
        {
            return false;
        }

        ///
        var savedWaterHeight = tube.CurrentWaterHeight;
        tube.CurrentWaterHeight = (int)Mathf.MoveTowards(tube.CurrentWaterHeight, tube.GlassHeight, ActiveTube.TopWaterHeight);
        givingTubes.Add(ActiveTube);
        State = TubeMovementState.Receiving;

        ///
        var transferAmount = tube.CurrentWaterHeight - savedWaterHeight;

        ///
        if (Mathf.Approximately(transferAmount, 0))
        {
            return false;
        }

        ///
        ActiveTube.tubeMovement.StartGivingTo(tube, transferAmount);

        ///
        return true;
    }

    private void StartGivingTo(Tube receiving_Tube, int amount)
    {
        ///
        StopShaking();
        
        ///
        var undoStep = new Undo_Step()
        {
            Amount = amount,
            gived_Tube = tube,
            received_Tube = receiving_Tube
        };
        EntryInGame.Instance.undoManager.RegisterStep(undoStep);

        ///
        StartCoroutine(GiveTo(receiving_Tube, amount));
    }

    private IEnumerator GiveTo(Tube receiving_Tube, int amount)
    {
        ///
        OnAnyTubeStartGiving?.Invoke();

        ///
        receiving_Tube.tubeMovement.fixedWaterCollider.enabled = true;
        receiving_Tube.tubeMovement.waterSqureCollider.enabled = true;

        ///
        waterWave.IsVisible = true;

        ///
        IsTransfering = true;

        ///
        if (ActiveTube == tube)
        {
            ActiveTube = null;
        }

        ///
        tube.CurrentWaterHeight -= amount;

        ///
        State = TubeMovementState.Giving;
        transfering_Water = false;
        finishedTransfering = false;

        ///
        this.receiving_Tube = receiving_Tube;

        waterWave.IsReversedX = receiving_Tube.transform.position.x >= initialX;

        ///
        var targetAngle = (receiving_Tube.transform.position.x < initialX) ? maxRotation : -maxRotation;
        float angle = 0;
        float targetY = receiving_Tube.transform.localPosition.y + GivingAdditionalY;
        while (!Mathf.Approximately(angle, targetAngle))
        {
            ///
            angle = Mathf.MoveTowardsAngle(angle, targetAngle, rotation_Speed * Time.deltaTime);
            glass.rotation = Quaternion.Euler(0, 0, angle);

            ///
            var edgePoint = ((receiving_Tube.transform.position.x < initialX) ? tubeView.normalTopLeft : tubeView.normalTopRight).position;
            var p = transform.localPosition;
            var targetX = p.x + receiving_Tube.transform.position.x - edgePoint.x;
            var targetPos = new Vector3(targetX, targetY, p.z);
            p = Vector3.MoveTowards(p, targetPos, move_Speed * Time.deltaTime);
            transform.localPosition = p;

            ///
            if (!transfering_Water)
            {
                if (Mathf.Approximately(p.x, targetX) && tubeView.IsWaterSpilling)
                {
                    StartCoroutine(TransferWater(amount));
                }
            }

            ///
            if (finishedTransfering)
            {
                break;
            }

            ///
            yield return null;
        }

        ///
        if (!transfering_Water)
        {
            StartCoroutine(TransferWater(amount));
        }

        ///
        while (!finishedTransfering)
        {
            yield return null;
        }

        ///
        tubeView.RoundLastWaterSegment();

        ///
        yield return StartCoroutine(BecomeIdle());

        ///
        waterWave.IsVisible = false;

        ///
        receiving_Tube.tubeMovement.fixedWaterCollider.enabled = false;
        receiving_Tube.tubeMovement.waterSqureCollider.enabled = false;

        ///
        IsTransfering = false;
    }

    private IEnumerator TransferWater(int amount)
    {
        ///
        transfering_Water = true;

        ///
        receiving_Tube.tubeMovement.waterWave.IsVisible = true;

        ///
        particleHit = false;

        ///
        onStartedGivingWater?.Invoke();

        ///
        while (!particleHit)
        {
            yield return null;
        }
        
        ///
        onStartedTransferringWater?.Invoke();

        ///
        float transferedAmount = 0;

        ///
        var topColorId = tubeView.TopWaterColorId;
        if (topColorId < 0)
        {
            throw new System.Exception();
        }

        ///
        while (transferedAmount < amount)
        {
            ///
            var deltaWater = Mathf.MoveTowards(transferedAmount, amount, water_Speed * Time.deltaTime) - transferedAmount;
            transferedAmount += deltaWater;

            ///
            tubeView.RemoveWater(deltaWater);
            receiving_Tube.tubeView.AddWater(topColorId, deltaWater);

            ///
            WaterHaptic.WaterTransferredThisFrame = true;
            
            ///
            yield return null;
        }

        ///
        finishedTransfering = true;

        ///
        receiving_Tube.tubeMovement.waterWave.IsVisible = false;

        ///
        receiving_Tube.tubeMovement.RemoveFromGivingList(tube);
        receiving_Tube = null;

        ///
        onFinishedGivingWater?.Invoke();
        
    }

    private void RemoveFromGivingList(Tube givingTube)
    {
        ///
        givingTubes.Remove(givingTube);

        ///
        if (givingTubes.Count == 0)
        {
            ///
            tubeView.RoundLastWaterSegment();
            State = TubeMovementState.Idle;

            ///
            if (tube.ColorCount == 1 && tube.CurrentWaterHeight == TubeData.GlassHeight)
            {
                IsCompleted = true;
                tubeView.UpdateNewVisual();
                onCompleted?.Invoke();
            }
        }
    }

    public void UpdateCompleteStatus()
    {
        IsCompleted = tube.ColorCount == 1 && tube.CurrentWaterHeight == TubeData.GlassHeight;
        tubeView.UpdateNewVisual();
        
    }

    public void HandlParticleHit()
    {
        particleHit = true;
    }

    public void Shake()
    {
        startShakingDelegate?.Invoke();
    }

    public void StopShaking()
    {
        stopShakingDelegate?.Invoke();
    }

    public void BecomeIdleImmediately()
    {
        ///
        if (ActiveTube == tube)
        {
            ActiveTube = null;
        }

        ///
        var p = transform.localPosition;
        p.x = initialX;
        p.y = initialY;
        transform.localPosition = p;

        ///
        State = TubeMovementState.Idle;
    }
}
