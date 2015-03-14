using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

[System.Serializable]
public class PlayerState {
    public List<CardController> hand;
    public List<CardState> deck;
    public int health;
    public int gold;
}

public class PlayerController : MonoBehaviour {

    public const int maxGold = 10;
    public const int maxHealth = 10;

    public PlayerState playerState;
    public GameObject cardModel;
    
    protected List<CardState>.Enumerator deckEnumerator;
    protected int turn;
    protected TextMeshPro healthText;
    protected TextMeshPro goldText;
    protected TextMeshPro timerText;

    protected Vector3 cardSpawnPosition;
    protected float handAngle;
    protected Vector3 pivot;
    protected float length;
    protected float spacing;
    protected float xRotation;

    protected PlayerDamageAnimation damageAnimation;
    protected PlayerDeathAnimation deathAnimation;

    public void PlayCard(CardController card) {
        playerState.hand.Remove(card);
        print(playerState.hand.Count);
        UpdateGold(playerState.gold - card.cardState.cost);
        UpdateHandPosition();
    }

    public void ReceiveDamage(int damage) {
        UpdateHealth(playerState.health - damage);
        gameObject.GetComponent<PlayerDamageAnimation>().Animate(damage);
        if (playerState.health <= 0) {
            gameObject.GetComponent<PlayerDeathAnimation>().Animate();
            // TODO end game here
        }
    }

    public void DrawCard() {
        // animation here
        if (!deckEnumerator.MoveNext()) {
            // TODO fatigue here?
            return;
        }
        cardModel.GetComponent<CardController>().cardState = deckEnumerator.Current;
        GameObject cardObject = Instantiate(cardModel, cardSpawnPosition, Util.CardRotation) as GameObject;
        AddCardToHand(cardObject.GetComponent<CardController>());
    }

    public void NewTurn() {
        turn += 1;
        DrawCard();
        UpdateGold(turn);
    }

    protected void Awake() {
        cardSpawnPosition = new Vector3(200, 10, -200);
        handAngle = 15;
        pivot = new Vector3(60, 1, -490);
        length = 400;
        spacing = 6;
        xRotation = 270;

        Init();
    }

    protected void Init() {
        Util.Shuffle(playerState.deck);
        deckEnumerator = playerState.deck.GetEnumerator();
        healthText = transform.Find("Health").GetComponent<TextMeshPro>();
        goldText = transform.Find("Gold").GetComponent<TextMeshPro>();
        timerText = transform.Find("Timer").GetComponent<TextMeshPro>();
        damageAnimation = gameObject.GetComponent<PlayerDamageAnimation>();
        deathAnimation = gameObject.GetComponent<PlayerDeathAnimation>();
        UpdateGold(0);
    }

    protected virtual void UpdateGold(int amount) {
        playerState.gold = Mathf.Min(maxGold, amount);
        goldText.text = playerState.gold.ToString();
        for (int i = 0; i < playerState.hand.Count; i++) {
            CardController card = playerState.hand[i];
            if (card.cardState.cost > playerState.gold) card.HideOutline();
            else card.ShowOutline();
        }
    }

    protected void UpdateHealth(int amount) {
        playerState.health = Mathf.Min(maxHealth, amount);
        healthText.text = playerState.health.ToString();
    }

    protected virtual void AddCardToHand(CardController card) {
        playerState.hand.Add(card);
        card.SetPlayerController(this);
        UpdateHandPosition();
    }
    
    protected void UpdateHandPosition() {
        // reposition all cards
        int numCards = playerState.hand.Count;
        float angle = spacing / 2 * (numCards - 1);
        if (angle > handAngle) angle = handAngle;
        float[] linspace = Util.Linspace(-angle, angle, numCards);
        float y = Util.CardSpawnHeight;
        for (int i = 0; i < numCards; i++) {
            Quaternion rotation = Quaternion.Euler(xRotation, 180 + linspace[i], 0);
            Vector3 position = (Quaternion.Euler(0, linspace[i], 0) * new Vector3(0, 0, length)) + pivot;
            position.y = y + i;
            CardController card = playerState.hand[i];
            if (!card.selected) card.Move(position, rotation);
            else card.MoveOnDrop(position, rotation);
        }
    }
}
