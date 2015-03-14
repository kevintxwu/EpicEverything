using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class AIController : PlayerController {

    public List<PieceController> pieces;
    public List<PieceController> opponentPieces;

    private float actionWait = 1;

    public void PlayCard(CardController card) {
        playerState.hand.Remove(card);
        UpdateGold(playerState.gold - card.cardState.cost);
        UpdateHandPosition();
    }

    protected void Awake() {
        cardSpawnPosition = new Vector3(-200, 10, 200);
        handAngle = 15;
        pivot = new Vector3(-60, 1, 510);
        length = -400;
        spacing = 6;
        xRotation = 90;

        Init();
        StartCoroutine(AIAction());
    }

    IEnumerator AIAction() {
        while (true) {
            List<PieceController> emptyPieces = new List<PieceController>();
            foreach (PieceController piece in pieces) {
                if (piece.CanAttack()) {
                    PieceController other = opponentPieces[Random.Range(0, opponentPieces.Count)];
                    Vector3[] intervals = Util.Linspace(piece.transform.position, other.transform.position, 7);
                    ArrowController arrow = ArrowController.Create(piece.transform.position);
                    for (int i = 1; i < intervals.Length; i++) {
                        arrow.UpdateTransform(intervals[i]);
                        yield return new WaitForSeconds(0.015f);
                    }
                    arrow.Snap(other.cardState == null);
                    yield return new WaitForSeconds(5 * Util.ArrowSnapWait);
                    piece.Attack(other);
                }
            }
            PieceController playablePiece = null;
            CardController playableCard = null;
            Util.Shuffle(pieces);
            foreach (CardController card in playerState.hand) {
                playablePiece = FindPlayablePiece(card);
                if (playablePiece != null) {
                    playableCard = card;
                    break;
                }
            }
            if (playableCard != null) {
                Vector3 position = new Vector3(
                    playablePiece.transform.position.x,
                    Util.CardHeight,
                    playablePiece.transform.position.z - 17);
                playableCard.PickupCard();
                playableCard.Move(position, playableCard.transform.rotation);
                yield return new WaitForSeconds(3*playableCard.repositionTime);
                playableCard.PlayCard(playablePiece);
            }
            yield return new WaitForSeconds(actionWait);
        }
    }

    PieceController FindPlayablePiece(CardController card) {
        foreach (PieceController piece in pieces) {
            if (card.IsPlayable(piece)) return piece;
        }
        return null;
    }

    protected override void UpdateGold(int amount) {
        playerState.gold = Mathf.Min(maxGold, amount);
        goldText.text = playerState.gold.ToString();
    }

    protected override void AddCardToHand(CardController card) {
        playerState.hand.Add(card);
        card.SetPlayerController(this);
        Destroy(card.gameObject.GetComponent<CardPreviewAnimation>());
        Destroy(card.gameObject.GetComponent<MoveCardWithMouse>());
        UpdateHandPosition();
    }

}
