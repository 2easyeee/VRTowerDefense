using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletImpact;
    ParticleSystem bulletEffect;
    AudioSource bulletAudio;

    public Transform crosshair;
    void Start()
    {
        bulletEffect = bulletImpact.GetComponent<ParticleSystem>();
        bulletAudio = bulletImpact.GetComponent<AudioSource>();

    }
    void Update()
    {
        ARAVRInput.DrawCrosshair(crosshair);
        if (ARAVRInput.GetDown(ARAVRInput.Button.IndexTrigger))
        {
            ARAVRInput.PlayVibration(ARAVRInput.Controller.RTouch); // Gun 방아쇠를 당겼을 때 컨트롤러 진동 재생

            bulletAudio.Stop();
            bulletAudio.Play();

            Ray ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);
            RaycastHit hitInfo;

            int playerLayer = 1 << LayerMask.NameToLayer("Player");
            int furnitureLayer = 1 << LayerMask.NameToLayer("Furniture");
            int layerMask = playerLayer | furnitureLayer;

            if (Physics.Raycast(ray, out hitInfo, 200, ~layerMask)) // ray가 부딪힌 정보는 hitInfo에 담긴다.
            {
                // 총알 이펙트가 진행되고 있으면 멈추고 재생
                bulletEffect.Stop();
                bulletEffect.Play();

                bulletImpact.position = hitInfo.point; // 부딪힌 지점 바로 위에서 이펙트가 보이도록 설정
                bulletImpact.forward = hitInfo.normal; // 부딪힌 지점의 방향으로 총알 이펙트의 방향을 설정
                /*
                if (hitInfo.transform.name.Contains("Drone"))
                {
                    DroneAI drone = hitInfo.transform.GetComponent<DroneAI>();
                    if (drone)
                    {
                        drone.OnDamageProcess();
                    }
                }
                */
            }
        }
    }
}
