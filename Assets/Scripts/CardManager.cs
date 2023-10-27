using UnityEngine;

namespace DefaultNamespace
{
    public class CardManager : MonoBehaviour
    {
        public static CardManager Instance { get; private set; }
        [SerializeField] private string rule; //all = all suits and all colors, color = red or black, same = same number and suit
        private readonly string _diamond = "red";
        private readonly string _heart = "red";
        private readonly string _club = "black";
        private readonly string _spade = "black";
        private string[] _suits = new string[2];
        private int[] _numbers = new int[2];
        private int[] _ids = new int[2];
        private int _arrayPos;
        
        private void Awake()
        {
            Instance = this;
        }

        public void Compare(string suit, int number, int id)
        {
            if (_arrayPos == 0)
            {
                _suits[_arrayPos] = suit;
                _numbers[_arrayPos] = number;
                _ids[_arrayPos] = id;
                _arrayPos++;
            }
            else
            {
                var temp = rule.ToLower();
                _suits[_arrayPos] = suit;
                _numbers[_arrayPos] = number;
                _ids[_arrayPos] = id;
                _arrayPos++;
                switch (temp)
                {
                    case "all":
                    {
                        if (_numbers[0] == _numbers[1])
                        {
                            BroadcastMessage("Deactivate",_ids);
                        }
                        BroadcastMessage("Reset");
                        _arrayPos = 0;
                        break;
                    }
                    case "color":
                    {
                        if (_numbers[0] == _numbers[1] && Color(_suits[0]).Equals(Color(_suits[1])))
                        {
                            BroadcastMessage("Deactivate",_ids);
                        }
                        BroadcastMessage("Reset");
                        _arrayPos = 0;
                        break;
                    }
                    case "same":
                    {
                        if (_numbers[0] == _numbers[1] && _suits[0].Equals(_suits[1]))
                        {
                            BroadcastMessage("Deactivate",_ids);
                        }
                        BroadcastMessage("Reset");
                        _arrayPos = 0;
                        break;
                    }
                }
            }
            
        }

        private string Color(string suitName)
        {
            var temp = suitName.ToLower();
            switch (temp)
            {
                case "club":
                    return _club;
                case "diamond":
                    return _diamond;
                case "heart":
                    return _heart;
                case "spade":
                    return _spade;
            }

            return "";
        }
    }
}