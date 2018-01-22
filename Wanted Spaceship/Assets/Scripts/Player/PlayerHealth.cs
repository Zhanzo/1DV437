using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public int currHealth;
    public Text healthText;
    public Slider healthSlider;
    public Image healthColor;
    private bool invincible = false;
    public GameObject hitDamage;

    private GameController controller;
    public GameObject playerExplosion;

    void Start () {
        healthSlider.value = currHealth;
        healthText.text = currHealth + "";

        GameObject tmp = GameObject.FindGameObjectWithTag("GameController");
        controller = tmp.GetComponent<GameController>();
        if (controller == null) {
            Debug.LogError("Unable to find the GameController script");
        }
    }

    public void DamagePlayer(int health) {
        if (currHealth - health <= 0) { // If the player's health reaches zero then the player is destroyed and the game is ended by the game controller
            currHealth = 0;
            Destroy(gameObject);
            GameObject tmp = Instantiate(playerExplosion, transform.position, transform.rotation);
            Destroy(tmp, 1);
            controller.EndGame();

        } else {
            GameObject tmp = Instantiate(hitDamage, transform.position, transform.rotation);
            Destroy(tmp, 1);
            currHealth = currHealth - health;
        }

        healthColor.color = Color.Lerp(Color.red, Color.green, (float) currHealth / 100); // Change health bar color tone on health remaining, looks nicer than static colors
        healthSlider.value = currHealth;
        healthText.text = currHealth + "";
    }

    public IEnumerator Invincible() {
        invincible = true;
        controller.ShowPowerup(0, 0, 255);
        healthColor.color = Color.gray;
        yield return new WaitForSeconds(10);
        controller.HidePowerup();
        healthColor.color = Color.Lerp(Color.red, Color.green, (float) currHealth / 100);
        invincible = false;
    }

    public bool IsInvincible() {
        return invincible;
    }
}
