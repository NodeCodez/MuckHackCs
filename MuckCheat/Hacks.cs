using UnityEngine;

namespace MuckCheat
{
    class Hacks : MonoBehaviour
    {
        public void OnGUI()
        {
            if (Input.GetKeyDown("h"))
            {
                PlayerStatus.Instance.hp = PlayerStatus.Instance.maxHp;
                PlayerStatus.Instance.hunger = PlayerStatus.Instance.maxHunger;
                PlayerStatus.Instance.stamina = PlayerStatus.Instance.maxStamina;
            }
            if (Input.GetKeyDown("j"))
            {
                PlayerStatus.Instance.currentSpeedArmorMultiplier = 25;
            }
            
            foreach (OnlinePlayer player in FindObjectsOfType(typeof(OnlinePlayer)) as OnlinePlayer[])
            {
                //In-Game Position
                Vector3 pivotPos = player.transform.position; //Pivot point NOT at the feet, at the center
                Vector3 playerFootPos; playerFootPos.x = pivotPos.x; playerFootPos.z = pivotPos.z; playerFootPos.y = pivotPos.y - 2f; //At the feet
                Vector3 playerHeadPos; playerHeadPos.x = pivotPos.x; playerHeadPos.z = pivotPos.z; playerHeadPos.y = pivotPos.y + 2f; //At the head

                //Screen Position
                Vector3 w2s_footpos = Camera.main.WorldToScreenPoint(playerFootPos);
                Vector3 w2s_headpos = Camera.main.WorldToScreenPoint(playerHeadPos);

                if (w2s_footpos.z > 0f)
                {
                    DrawBoxESP(w2s_footpos, w2s_headpos, Color.red);
                }
            }
        }


        public void DrawBoxESP(Vector3 footpos, Vector3 headpos, Color color) //Rendering the ESP
        {
            float height = headpos.y - footpos.y;
            float widthOffset = 2f;
            float width = height / widthOffset;

            //ESP BOX
            Render.DrawBox(footpos.x - (width / 2), (float)Screen.height - footpos.y - height, width, height, color, 2f);

            //Snapline
            Render.DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(footpos.x, (float)Screen.height - footpos.y), Color.green, 2f);
        }

    public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Loader.Unload();
            }
        }

    }
}