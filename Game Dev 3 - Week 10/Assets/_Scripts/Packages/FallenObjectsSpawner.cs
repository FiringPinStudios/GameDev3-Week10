using GameDevWithMarco.DesignPattern;
using System.Collections;
using UnityEngine;

namespace GameDevWithMarco.Managers
{
    public class FallenObjectsSpawner : MonoBehaviour
    {
        [Header("Packages Spawn Position")]
        [SerializeField] GameObject[] spawners;
        [Header("Package Delay Variables")]
        [SerializeField] float initialDelay = 2.0f;
        [SerializeField] float minDelay = 0.5f;
        [SerializeField] float delayIncreaseRata = 0.1f;
        float currentDelay;
        [Header("Packages Drop Chance Percentages")]
        [SerializeField] float goodPackageDropPercentage;
        [SerializeField] float badPackageDropPercentage;
        [SerializeField] float lifePackageDropPercentage;

        [SerializeField] float minimum_goodPackageDropPercentage;
        [SerializeField] float maximum_badPackageDropPercentage;
        [SerializeField] float percentageChangeRatio = 0.1f;

        private void Start()
        {
            StartCoroutine(SpawningLoop());
        }

        private void SpawnPackageAtRandomLocation(ObjectPoolingPattern.TypeOfPool poolType)
        {
            GameObject spawnedPackage = ObjectPoolingPattern.Instance.GetPoolItem(poolType);

            int randomInteger = Random.Range(0, spawners.Length - 1);

            Vector2 spawnPoisiton = spawners[randomInteger].transform.position;

            spawnedPackage.transform.position = spawnPoisiton;
        }

        private IEnumerator SpawningLoop()
        {
            SpawnPackageAtRandomLocation(GetPackageTypeBasedOnPercentage());

            yield return new WaitForSeconds(currentDelay);

            currentDelay -= delayIncreaseRata;

            if (currentDelay < minDelay)
                currentDelay = minDelay;

            StartCoroutine(SpawningLoop());
        }


        private ObjectPoolingPattern.TypeOfPool GetPackageTypeBasedOnPercentage()
        {
            float randomValue = Random.Range(0f, 100.1f);

            if (randomValue <= goodPackageDropPercentage)
            {
                return ObjectPoolingPattern.TypeOfPool.Good;
            }
            else if (randomValue <= goodPackageDropPercentage &&
                randomValue <= (goodPackageDropPercentage + badPackageDropPercentage))
            {
                return ObjectPoolingPattern.TypeOfPool.Bad;
            }
            else
            {
                return ObjectPoolingPattern.TypeOfPool.Life;
            }
        }

        private void CaptThePercentages()
        {
            if (goodPackageDropPercentage <= minimum_goodPackageDropPercentage &&
                badPackageDropPercentage >= maximum_badPackageDropPercentage)
            {
                goodPackageDropPercentage = minimum_goodPackageDropPercentage;
                badPackageDropPercentage = maximum_badPackageDropPercentage;
            }
        }

        public void GrowBadPercentage()
        {
            goodPackageDropPercentage -= percentageChangeRatio;
            badPackageDropPercentage += percentageChangeRatio;
            CaptThePercentages();
        }

        public void GrowGoodPercentage()
        {
            goodPackageDropPercentage += percentageChangeRatio;
            badPackageDropPercentage -= percentageChangeRatio;
            CaptThePercentages();
        }
    }
}
