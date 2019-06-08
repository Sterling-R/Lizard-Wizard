using UnityEngine;
using System.Collections;
 
public class Spark : MonoBehaviour {
    public GameObject target;
    private LineRenderer lineRend;
    [SerializeField] float arcLength;
    [SerializeField] float arcVariation;
    [SerializeField] float inaccuracy;
    [SerializeField] float timeOfZap;
    int i;
 
    void Start () {
		Debug.Log(target);
        lineRend = gameObject.GetComponent<LineRenderer>();
        lineRend.positionCount = 1;
        ZapTarget(target);
        i = 1;
        lineRend.SetPosition (0, transform.position);//make the origin of the LR the same as the transform
    }
 
    void Update() 
    {
        
                    Vector3 lastPoint = transform.position; 
                    lineRend.positionCount = i + 1;//then we need a new vertex in our line renderer
                    Vector3 fwd = target.transform.position - lastPoint;//gives the direction to our target from the end of the last arc
                    fwd.Normalize ();//makes the direction to scale
                    fwd = Randomize (fwd, inaccuracy);//we don't want a straight line to the target though
                    fwd *= Random.Range (arcLength * arcVariation, arcLength);//nature is never too uniform
                    fwd += lastPoint;//point + distance * direction = new point. this is where our new arc ends
                    lineRend.SetPosition (i, fwd);//this tells the line renderer where to draw to
                    i++;
                    lastPoint = fwd;//so we know where we are starting from for the next arc
                    lineRend.positionCount = i + 1;
                    lineRend.SetPosition (i, target.transform.position);
                    gameObject.transform.position = lineRend.GetPosition(i);
     }
 
    private Vector3 Randomize (Vector3 newVector, float devation) {
        newVector += new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) * devation;
        newVector.Normalize();
        return newVector;
    }
 
    public void ZapTarget( GameObject newTarget){
        target = newTarget;
    }
}