using UnityEngine;

public class Unit : MonoBehaviour
{
    public AnimationCurve animationCurve;
    private float unitSpeed=1.5f;
    public GameObject usableMotherCell;
    private Vector2 usableUnitPosition;
    private float time = 0;
    public SpriteRenderer unitSprite;

    private void Update()
    {
        transform.Translate(Vector2.up * animationCurve.Evaluate(time /0.5f)* unitSpeed * Time.deltaTime);
        time += Time.deltaTime;
    }

    public void SetTarget(GameObject usableMotherCell) 
    {
        this.usableMotherCell = usableMotherCell;
        usableUnitPosition = usableMotherCell.transform.position - transform.position;
        usableUnitPosition = usableUnitPosition.normalized;
        float angle = Vector2.Angle(Vector2.up, usableUnitPosition);
        if(transform.position.x<usableMotherCell.transform.position.x) transform.Rotate(Vector3.forward, -angle);
        else transform.Rotate(Vector3.forward, angle);
    }
}
