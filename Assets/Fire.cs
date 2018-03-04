﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
    public float angle; //theta
    public float gunangle; // angle of the gun
    public float GunVelocityInitial;
    public float GunVelocity;
    public float speed;
    private float gravity = 9.81f;
    public GameObject gunball;
    public float timeelapsed;
    public float positionZ;
    public float positionY;
    public float frame;
    public float cAngle;
    public float sAngle;
    public float tick;
    public float range;

    public float Rad;
    public float degree;
    public float Alpha;
    public float TwoAlpha;
    public float positionX;
    public float CAlpha;
    public float SAlpha;

    public float Gamma;
    public float CGamma;
    public float SGamma;

    public float SpeedYI;
    public float SpeedY;

    public float OmegaI;
    public float OmegaF;
    public float AngularAlpha; // represents Angular acceleration
    public float Theta;
    public float AngularDegree;
    public float FTheta;

    public float Kilogram;
    public float Cd;
    public float Tau;
    public float Vw;
    public float Cw;
   
	// Use this for initialization
	void Start () {
        angle = (Mathf.Asin(gravity * (GameObject.Find("Target").transform.position.z - GameObject.Find("GunCentre").transform.position.z) / Mathf.Pow(speed,2))/2 ) ;
        gunangle = angle * 180 / Mathf.PI;
        cAngle = Mathf.Cos(gunangle * Mathf.PI/ 180);
        sAngle = Mathf.Sin(gunangle * Mathf.PI / 180);
        
       
        Instantiate(gunball, GameObject.Find("GunCentre").transform.position, Quaternion.identity);

       
        Rad = Mathf.Asin(gravity * (Mathf.Sqrt(Mathf.Pow(GameObject.Find("Target").transform.position.z - GameObject.Find("GunCentre").transform.position.z, 2) + Mathf.Pow(GameObject.Find("Target").transform.position.x, 2)) / Mathf.Pow(speed, 2)));
        degree = Rad * (180 / Mathf.PI);
        Alpha = (180 - degree) / 2;
        

        CAlpha = Mathf.Cos(Alpha * Mathf.PI / 180);
        SAlpha = Mathf.Sin(Alpha * Mathf.PI / 180);
        
        Gamma = Mathf.Asin(GameObject.Find("Target").transform.position.x / Mathf.Sqrt(Mathf.Pow(GameObject.Find("Target").transform.position.z - GameObject.Find("GunCentre").transform.position.z, 2) + Mathf.Pow(GameObject.Find("Target").transform.position.x , 2) ));
        CGamma = Mathf.Cos(Gamma);
        SGamma = Mathf.Sin(Gamma);
        GameObject.Find("GunCentre").transform.eulerAngles = new Vector3(-gunangle, Gamma * 180 / Mathf.PI, 0);
        SpeedYI = speed * CAlpha;

        Tau = Kilogram / Cd;


    }
	
	// Update is called once per frame
	void FixedUpdate () {

        
        timeelapsed = Time.fixedDeltaTime * frame;
        frame++;
        tick += Time.deltaTime * frame;
       // positionZ = GameObject.Find("GunCentre").transform.position.z + (speed * SAlpha * CGamma) * timeelapsed;

        positionZ = (speed * Tau * (1 - Mathf.Exp(-timeelapsed / Tau))) + (Vw * Tau * (1 - Mathf.Exp(-timeelapsed / Tau)))  - (Vw * timeelapsed); // project 7 Q 3a
        speed = (Mathf.Exp(-timeelapsed / Tau) * speed) + ((Mathf.Exp(-timeelapsed/Tau) - 1) * Vw); // project 7 Q 3a



     /*   SpeedY = SpeedYI - gravity * timeelapsed;
        positionY = (SpeedY * timeelapsed) - ((-gravity * Mathf.Pow(timeelapsed, 2) / 2));
        positionX = GameObject.Find("GunCentre").transform.position.x + (speed * SAlpha * SGamma) * timeelapsed;   */

        positionY = SpeedYI + (SpeedY * Tau * (1 - Mathf.Exp(-timeelapsed / Tau))) + (gravity * (Mathf.Pow(Tau, 2)) * (1 - Mathf.Exp(-timeelapsed / Tau))) - (gravity*Tau*timeelapsed);  // project 7 Q 3b
        SpeedY = (Mathf.Exp(-timeelapsed / Tau) * SpeedY) + ((Mathf.Exp(-timeelapsed / Tau) - 1) * (gravity * Tau) ); // project 7 Q 3b

        positionX = ((speed * Tau) * (1 - Mathf.Exp(-timeelapsed / Tau))) +  ( (Vw * Tau) * (1 - Mathf.Exp(-timeelapsed / Tau))  ) - (Vw * timeelapsed)  ; // project 7 Q 3c





        Theta = (OmegaF * Time.deltaTime) + ((AngularAlpha * Time.deltaTime * Time.deltaTime) / 2f); 
        OmegaF = OmegaI + AngularAlpha * timeelapsed;
        FTheta += Theta;

        AngularDegree = Theta * Mathf.Rad2Deg;
   
        
        GameObject.Find("Gunball(Clone)").transform.position = new Vector3(positionX,positionY,positionZ);
       // GameObject.Find("Gunball(Clone)").transform.eulerAngles = new Vector3(-AngularDegree, 0);
       // GameObject.Find("Gunball(Clone)").transform.Rotate(Vector3.right * -AngularDegree);

        if (positionY <= 0.05 && tick > 0.5) {
            UnityEditor.EditorApplication.isPaused = true;
        }

	}
}
