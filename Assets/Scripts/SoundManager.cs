using UnityEngine;

namespace TowerDefense
{
    public class SoundManager : Loader <SoundManager>
    {
        [SerializeField]
        private AudioClip _arrow;
        [SerializeField]
        private AudioClip _death;
        [SerializeField]
        private AudioClip _fireBall;
        [SerializeField]
        private AudioClip _gameOver;
        [SerializeField]
        private AudioClip _hit;
        [SerializeField]
        private AudioClip _levelUp;
        [SerializeField]
        private AudioClip _newGame;
        [SerializeField]
        private AudioClip _rock;
        [SerializeField]
        private AudioClip _towerBuilt;

        public AudioClip Arrow
        {
            get
            {
                return _arrow;
            }
        }

        public AudioClip Death
        {
            get
            {
                return _death;
            }
        }
        public AudioClip FireBall
        {
            get
            {
                return _fireBall;
            }
        }

        public AudioClip GameOver
        {
            get
            {
                return _gameOver;
            }
        }
        public AudioClip Hit
        {
            get
            {
                return _hit;
            }
        }
        public AudioClip LevelUp
        {
            get
            {
                return _levelUp;
            }
        }
        public AudioClip NewGame
        {
            get
            {
                return _newGame;
            }
        }
        public AudioClip Rock
        {
            get
            {
                return _rock;
            }
        }
        public AudioClip TowerBuilt
        {
            get
            {
                return _towerBuilt;
            }
        }
    }
}