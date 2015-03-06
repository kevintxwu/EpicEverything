using UnityEngine;
using System.Collections;
using TMPro;

[System.Serializable]
public class CardState {
    public int attack;
    public int health;
    public int time;
    public int cost;
    public bool ranged;
    public bool speed;
    public bool block;

    // temporary
    public Material cardMaterial;
    public Material pieceMaterial;
}

public class CardController : MonoBehaviour {

    Quaternion baseRotation = Quaternion.Euler(270, 180, 0);

    public CardState cardState;
    public float repositionTime;
    public float returnTime;

    public bool selected {get; private set;}

    private PlayerController player;
    private MoveToTransform mover;
    private Vector3 position;
    private Quaternion rotation;

    public void PickupCard() {
        position = mover.position;
        rotation = mover.rotation;
        mover.moving = false;
        transform.rotation = baseRotation;
        selected = true;
    }

    public void PlayCard(PieceController piece) {
        player.PlayCard(this);
        selected = false;
        gameObject.GetComponent<CardPlayAnimation>().Animate(piece, cardState.cost);
    }

    public bool IsPlayable(PieceController piece) {
        if (piece.cardState != null ||
            piece.player != player ||
            player.playerState.gold < cardState.cost) return false;
        return true;
    }

    public void MoveInHand(Vector3 position, Quaternion rotation) {
        MoveToTransform(position, rotation, repositionTime);
    }


    public void MoveInHandOnDrop(Vector3 position, Quaternion rotation) {
        this.position = position;
        this.rotation = rotation;
    }

    public void ReturnToHand() {
        selected = false;
        MoveToTransform(position, rotation, returnTime);
    }

    public void SetPlayerController(PlayerController player) {
        this.player = player;
    }

    public void ShowOutline() {
        transform.Find("OutlineParticle").gameObject.active = true;
    }

    public void HideOutline() {
        transform.Find("OutlineParticle").gameObject.active = false;
    }

    void Awake() {
        renderer.material = cardState.cardMaterial;
        mover = gameObject.GetComponent<MoveToTransform>();
        transform.Find("Attack").GetComponent<TextMeshPro>().text = cardState.attack.ToString();
        transform.Find("Health").GetComponent<TextMeshPro>().text = cardState.health.ToString();
        transform.Find("Cost").GetComponent<TextMeshPro>().text = cardState.cost.ToString();
        transform.Find("Time").GetComponent<TextMeshPro>().text = cardState.time.ToString();
    }

    void MoveToTransform(Vector3 position, Quaternion rotation, float time) {
        mover.Move(position, rotation, time);
    }
}
