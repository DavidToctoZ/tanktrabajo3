using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ULTanksZombies
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;

        public static GameManager Instance { get { return instance; } }

        public GameObject[] zombies;
        public float spawnZombieTime = 1f;

        private float timerToSpawnZombie = 0f;

        public float timeBetweenHordes = 5.0f;
        public float timeHorde = 30.0f;
        public int progessionSpawnZombie = 5;

        public Text zombieText;
        public Text killsText;
        public Text rondaText;
        public Text textLabelTiempo;
        public Text tiempoSiguienteRondaText;
        public Text beginText;

        public Transform tank;

        int initialNumberHordeZombies = 2;
        public int numKillsZombie = 0;
        private void Start()
        {

            firstHorde();
        }


        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            
        }

        public int cantZombies = 2;
        int numRound = 1;
        public int totalKills = 0;
        private void Update()
        {
            zombieText.text = cantZombies.ToString();
            killsText.text = totalKills.ToString();
            rondaText.text = numRound.ToString();
            tiempoSiguienteRondaText.text = timeRemaining.ToString("f2");
            horde();
            if (timeRemaining > 0 && activarTimer == true)
            {
                timeRemaining -= Time.deltaTime;
                
            }

           

        }
       
        private void firstHorde(){
            
             SpawnZombie();
             SpawnZombie();       
        }

        bool startHorde = true;
        bool firstHordeBool = true;
        private void horde()
        {
            if (numKillsZombie == initialNumberHordeZombies && startHorde == true)
            {
                Debug.Log("Begin Horde");
                
                initialNumberHordeZombies = initialNumberHordeZombies + progessionSpawnZombie;
                StartCoroutine("WaitNextRound");

                startHorde = false;
            }
            else if(numKillsZombie == initialNumberHordeZombies)
            {
                startHorde = true;
            }
            else
            {
                startHorde = false;
            }
            
        }
        bool activarTimer = false;
        float timeRemaining = 5;

        IEnumerator WaitNextRound()
        {
            if (numKillsZombie == 2 && firstHordeBool == true)
            {
                numKillsZombie = 0;
                firstHordeBool = false;
                Debug.Log("ACA");
            }
            numKillsZombie = 0;
            textLabelTiempo.gameObject.SetActive(true);
            tiempoSiguienteRondaText.gameObject.SetActive(true);
            beginText.gameObject.SetActive(true);
            activarTimer = true;
            timeRemaining = 5;
            
            yield return new WaitForSeconds(timeBetweenHordes);
            activarTimer = false;
            textLabelTiempo.gameObject.SetActive(false);
            tiempoSiguienteRondaText.gameObject.SetActive(false);
            beginText.gameObject.SetActive(false);
            cantZombies = initialNumberHordeZombies;
            numRound = numRound + 1;
            
            for (int i = 0; i < initialNumberHordeZombies; i++)
            {
                SpawnZombie();
            }
            
        }


        private void SpawnZombie()
        {
            int posZombies = Random.Range(0, zombies.Length);
            Vector3 spawnPosition = new Vector3(
                Random.Range(tank.position.x - 50f , tank.position.x + 50),
                0.5f,
                Random.Range(tank.position.z - 50f, tank.position.z + 50)
            );
            Instantiate(zombies[posZombies], spawnPosition, Quaternion.identity);
        }
    }
}
