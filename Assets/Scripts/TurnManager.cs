using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : Singleton<TurnManager>
{

    public enum EPhaseType { NONE, CONSTRUCT, RECRUIT, FIGHT };

    public enum ETurnType { NONE, IADECISION, PLAYERDECISION, ACTION, CONSEQUENCES, FIGHT };

    #region Fields
    private EPhaseType _phase = EPhaseType.CONSTRUCT;
    private ETurnType _turn = ETurnType.NONE;


    private List<Tile> _targetList = null;

    IEnumerator _fireCoroutine;
    #endregion Fields

    #region Properties
    public EPhaseType Phase => _phase;
    public ETurnType Turn => _turn;

    public List<Tile> TargetList
    {
        get
        {
            return _targetList;
        }
        set
        {
            _targetList = value;
        }
    }
    #endregion Properties

    #region Methods


    void Start()
    {
        _targetList = new List<Tile>();
    }

    void Update()
    {
        switch (_phase)
        {
            case EPhaseType.CONSTRUCT:

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _phase = EPhaseType.RECRUIT;
                }

                break;

            case EPhaseType.RECRUIT:

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _phase = EPhaseType.FIGHT;
                    FightStart();

                }
                break;

            case EPhaseType.FIGHT:

                if (Input.GetKeyDown(KeyCode.Space))
                {

                    EndTurn();

                }
                break;
        }
    }
    private void FightStart()
    {

    }

    private void EndTurn()
    {
        if (_targetList.Count > 0)
        {
            for (int i = 0; i < _targetList.Count; i++)
            {
                _fireCoroutine = Fire(1.5f, i);
                StartCoroutine(_fireCoroutine);

            }
        }
    }

    private void Critical()
    {

    }

    private void Fire(Tile tileTargeted)
    {
        int diceResult = Random.Range(1, 8);
        Debug.Log("Dice Result : " + diceResult);

        switch (diceResult)
        {
            case 1:
                tileTargeted.TakeDamage(1);
                break;

            case 2:
                tileTargeted.TakeDamage(1);

                break;

            case 3:
                tileTargeted.GetNeighbour(ENeighbourType.TOP).TakeDamage(1);

                break;

            case 4:
                tileTargeted.GetNeighbour(ENeighbourType.RIGHT).TakeDamage(1);

                break;

            case 5:
                tileTargeted.GetNeighbour(ENeighbourType.BOTTOM).TakeDamage(1);

                break;

            case 6:
                tileTargeted.GetNeighbour(ENeighbourType.LEFT).TakeDamage(1);

                break;
            case 7:
                Critical();
                break;
        }
    }

    private IEnumerator Fire(float delay, int i)
    {
        //DICE SOUND EFFECT
        yield return new WaitForSeconds(delay);
        //FIRE SOUND EFFECT
        //SMALL DELAY
        Fire(_targetList[i]);


    }
    #endregion Methods

}
