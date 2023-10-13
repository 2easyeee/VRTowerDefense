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
            ARAVRInput.PlayVibration(ARAVRInput.Controller.RTouch); // Gun ��Ƽ踦 ����� �� ��Ʈ�ѷ� ���� ���

            bulletAudio.Stop();
            bulletAudio.Play();

            Ray ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);
            RaycastHit hitInfo;

            int playerLayer = 1 << LayerMask.NameToLayer("Player");
            int furnitureLayer = 1 << LayerMask.NameToLayer("Furniture");
            int layerMask = playerLayer | furnitureLayer;

            if (Physics.Raycast(ray, out hitInfo, 200, ~layerMask)) // ray�� �ε��� ������ hitInfo�� ����.
            {
                // �Ѿ� ����Ʈ�� ����ǰ� ������ ���߰� ���
                bulletEffect.Stop();
                bulletEffect.Play();

                bulletImpact.position = hitInfo.point; // �ε��� ���� �ٷ� ������ ����Ʈ�� ���̵��� ����
                bulletImpact.forward = hitInfo.normal; // �ε��� ������ �������� �Ѿ� ����Ʈ�� ������ ����
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
