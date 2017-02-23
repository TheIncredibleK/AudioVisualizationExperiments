using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ReadSoundData : MonoBehaviour {


	AudioSource audio;
	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update( )
	{
		
	}


	public float[] CurrentSample() {
		float[] spectrum = new float[128];
		float[] currents = new float[2];
		AudioListener.GetSpectrumData( spectrum, 1, FFTWindow.BlackmanHarris );
		float total = 0.0f;
		for (int i = 0; i < (128/4); i++) {
			total += spectrum [i];
		}

		float currentDrum = total/(128/4);
		total = 0.0f;
		for (int i = 128 / 4; i < (128 / 4) * 2; i++) {
			total += spectrum [i];
		}
		float currentMel = total/(128/4);
		currents [0] = currentDrum;
		currents [1] = currentMel;
		return spectrum;
		//for( int i = 1; i < spectrum.Length-1; i++ )
		//{
			//Drum
			//Debug.DrawLine( new Vector3( i - 1, spectrum[i] + 10, 0 ), new Vector3( i, spectrum[i + 1] + 10, 0 ), Color.red );

			//Debug.Log("Drum Val :" + ((spectrum[i] + 10) - (spectrum[i-1] + 10)));
			//Melody and Drum
			//Debug.DrawLine( new Vector3( i - 1, Mathf.Log( spectrum[i - 1] ) + 10, 2 ), new Vector3( i, Mathf.Log( spectrum[i] ) + 10, 2 ), Color.cyan );
			//Drum and Bass?
			//Debug.DrawLine( new Vector3( Mathf.Log( i - 1 ), spectrum[i - 1] - 10, 1 ), new Vector3( Mathf.Log( i ), spectrum[i] - 10, 1 ), Color.green );

			//Debug.DrawLine( new Vector3( Mathf.Log( i - 1 ), Mathf.Log( spectrum[i - 1] ), 3 ), new Vector3( Mathf.Log( i ), Mathf.Log( spectrum[i] ), 3 ), Color.blue );
		//}

		
	}
}
