using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class StreakSystem : MonoBehaviour
{
    [Header("Streak Settings")]
    public int streakToActivate = 10;          // nombre de notes pour charger le bonus
    private int currentStreak = 0;
    private bool bonusReady = false;

    [Header("Bonus Settings")]
    public float bonusDuration = 5f;           // durée du bonus
    private bool bonusActive = false;
    private float bonusTimer = 0f;

    [Header("Bonus Effect")]
    public float damageMultiplier = 2f;        // x2 dégâts quand le bonus est actif

    private InputAction bonusAction;

    // Events pour les autres scripts (BonusBar, UI texte, attaques)
    public static event Action<float> OnStreakProgress;          // valeur entre 0 et 1 pour la barre
    public static event Action<string> SignalText;               // messages texte
    public static event Action<float> OnBonusDamageMultiplier;   // multiplicateur de dégâts

    void OnEnable()
    {
        // S'abonner aux events du ButtonManager
        ButtonManager.OnNoteSuccess += HandleNoteSuccess;
        ButtonManager.OnNoteFail += HandleNoteFail;
    }

    void OnDisable()
    {
        //ButtonManager.OnNoteSuccess -= HandleNoteSuccess;
        //ButtonManager.OnNoteFail -= HandleNoteFail;
    }

    void Awake()
    {
        // Crée l'action bonus (touche Espace)
       

        bonusAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/space");
        bonusAction.performed += OnBonus;
        bonusAction.Enable();
    }

    void OnDestroy()
    {
        bonusAction.performed -= OnBonus;
        bonusAction.Disable();
    }

    private void HandleNoteSuccess()
    {
        if (bonusActive) return;

        currentStreak++;
        float progress = Mathf.Clamp01((float)currentStreak / streakToActivate);
        OnStreakProgress?.Invoke(progress);


        if (currentStreak >= streakToActivate)
        {
            bonusReady = true;
            SignalText?.Invoke("Bonus Ready!");
        }
    }

    private void HandleNoteFail()
    {
        currentStreak = 0;
        bonusReady = false;
        OnStreakProgress?.Invoke(0f);
    }

    private void OnBonus(InputAction.CallbackContext ctx)
    {
        if (bonusReady && !bonusActive)
        {
            bonusActive = true;
            bonusReady = false;
            currentStreak = 0;
            bonusTimer = bonusDuration;

            OnStreakProgress?.Invoke(0f);
            SignalText?.Invoke("Bonus Activated!");

            // Appliquer le multiplicateur de dégâts
            OnBonusDamageMultiplier?.Invoke(damageMultiplier);
        }
    }

    void Update()
    {
        if (bonusActive)
        {
            bonusTimer -= Time.deltaTime;
            if (bonusTimer <= 0f)
            {
                bonusActive = false;
                SignalText?.Invoke("Bonus Ended.");

                // Revenir au multiplicateur normal
                OnBonusDamageMultiplier?.Invoke(1f);
            }
        }
    }

    // getters pour d'autres scripts si nécessaire
    public int CurrentStreak => currentStreak;
    public int StreakToActivate => streakToActivate;
    public bool BonusReady => bonusReady;
    public bool BonusActive => bonusActive;
}
