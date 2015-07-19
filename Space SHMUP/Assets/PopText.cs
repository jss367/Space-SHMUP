using UnityEngine;
using System.Collections;

public class PopText : MonoBehaviour
{

	public GameObject popText;

	public float lifetime = .5f;	
	
	void Start ()
	{
		Destroy (gameObject, lifetime);
		Vector3 pos = popText.transform.position;
		pos.z = -9;
		popText.transform.position = pos;
	}
}