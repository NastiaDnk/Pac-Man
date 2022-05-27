using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacMan
{
    public class GameManager : MonoBehaviour
    {
        public Ghost[] ghosts;
        public Pacman pacman;
        public Transform pellets;

        public int ghostMultiplier { get; private set; }

        public int Score { get; private set; }
        public int Lives { get; private set; }

        private void Start()
        {
            NewGame();
        }

        private void Update()
        {
            if (Input.anyKeyDown && Lives == 0)
            {
                NewGame();
            }
        }

        private void NewGame()
        {
            SetScore(0);
            SetLives(3);
            NewRound();
        }

        private void SetScore(int score)
        {
            Score = score;
        }

        private void SetLives(int lives)
        {
            Lives = lives;
        }

        private void NewRound()
        {
            foreach (Transform p in pellets)
            {
                p.gameObject.SetActive(true);
            }

            ResetState();
        }

        private void ResetState()
        {
            ResetGhostMultiplier();
            for (int i = 0; i < ghosts.Length; ++i)
            {
                ghosts[i].gameObject.SetActive(true);
            }

            pacman.gameObject.SetActive(true);
        }

        private void GameOver()
        {
            for (int i = 0; i < ghosts.Length; ++i)
            {
                ghosts[i].gameObject.SetActive(false);
            }

            pacman.gameObject.SetActive(false);
        }

        public void GhostEaten(Ghost ghost)
        {
            SetScore(Score + ghost.points * ghostMultiplier);
            ghostMultiplier++;
        }

        public void PacmanEaten()
        {
            pacman.gameObject.SetActive(false);
            SetLives(Lives - 1);
            if (Lives > 0)
            {
                Invoke(nameof(ResetState), 3.0f);
            }
            else
            {
                GameOver();
            }
        }

        public void PelletEaten(Pellet pellet)
        {
            pellet.gameObject.SetActive(false);
            SetScore(Score + pellet.points);

            if (!HasRemainingPellets())
            {
                pacman.gameObject.SetActive(false);
                Invoke(nameof(NewRound), 3.0f);
            }
        }

        public void PowerPelletEaten(PowerPellet powerPellet)
        {
            PelletEaten(powerPellet);
            CancelInvoke();
            Invoke(nameof(ResetGhostMultiplier), powerPellet.duration);
        }

        private bool HasRemainingPellets()
        {
            foreach (Transform p in pellets)
            {
                if (p.gameObject.activeSelf)
                    return true;

            }
            return false;
        }

        private void ResetGhostMultiplier()
        {
            ghostMultiplier = 1;
        }
    }
}

