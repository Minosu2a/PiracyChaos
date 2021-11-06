using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    private enum EOccupationType { NONE, BLOCK, CREW };

    #region Fields


    [Header("Creation")]
    [SerializeField] private Color _defaultColor, _offsetColor;
    [SerializeField] private MeshRenderer _tileRenderer = null;
    private int _z;
    private int _y;
    
    [Header("Mouse Over")]
    [SerializeField] private GameObject _highlight = null;
    private bool _mouseOver = false;


    [Header("Population")]
    [SerializeField] private bool _isFull = false;
    [SerializeField] private EOccupationType _occupationType = EOccupationType.NONE;
    [SerializeField] private GameObject _block = null;
    [SerializeField] private bool _explosivePlaced = false;


    [Header("Crew")]
    [SerializeField] private MeshRenderer _crewRenderer = null;

    [Header("Target")]
    [SerializeField] private GameObject _target = null;
    private bool _targetPlaced = false;

    #endregion Fields

    #region Properties
    #endregion Properties

    #region Methods

    public void Init(bool isOffset, int z, int y)
    {
        if(isOffset == true)
        {
            _tileRenderer.material.color = _offsetColor;
        }
        else
        {
            _tileRenderer.material.color = _defaultColor;
        }

        _z = z;
        _y = y;
    }

    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
        _mouseOver = true;
    }

    private void OnMouseExit()
    {
        _highlight.SetActive(false);
        _mouseOver = false;
        RemoveTarget();
    }

    private void PlaceBlock()
    {
        _isFull = true;
        _occupationType = EOccupationType.BLOCK;
        _block.SetActive(true);
    }
    private void PlaceCrew()
    {
        _isFull = true;
        _occupationType = EOccupationType.CREW;
        _crewRenderer.gameObject.SetActive(true);
    }

    private void PlaceTarget()
    {
        _targetPlaced = true;
        TurnManager.Instance.TargetList.Add(this);
    }

    private void RemoveTarget()
    {
        if(_targetPlaced == false)
        {
            _target.SetActive(false);
        }
    }

    private void Update()
    {
        if(_mouseOver == true)
        {
           
                switch (TurnManager.Instance.Phase)
                {
                    case TurnManager.EPhaseType.CONSTRUCT:

                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            PlaceBlock();
                        }

                        break;

                    case TurnManager.EPhaseType.RECRUIT:

                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            if(_isFull == false)
                            {
                                PlaceCrew();
                            }
                            else
                            {
                                Debug.Log("Tile Occupied");
                            }
                        }

                    break;

                    case TurnManager.EPhaseType.FIGHT:

                    _target.SetActive(true);

                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                             PlaceTarget();
                        }

                    break;
                }
         
        }
    }


    #region public Methods

    public void EmptyTile()
    {
        _crewRenderer.gameObject.SetActive(false);
        //FUNNY SOUND OF THE PIRATE DYING LMAAAAAAAAAAAO
        _isFull = false;
    }

    public void TakeDamage(int damage)
    {
        if(_isFull == true)
        {
            if(_explosivePlaced == true)
            {
                //Fonction Explosion
            }
            else
            {
                switch (_occupationType)
                {
                    case EOccupationType.BLOCK:
                        _block.SetActive(false);
                        break;
                    case EOccupationType.CREW:
                        _crewRenderer.gameObject.SetActive(false);
                        break;
                }
            }

        
        }
    }

    public Tile GetNeighbour(ENeighbourType neighbour)
    {
        switch(neighbour)
        {
            case ENeighbourType.TOP:
                return GridManager.Instance.GetTileWithVector(new Vector2( _z , _y + 1 ));
                break;

            case ENeighbourType.BOTTOM:
                return GridManager.Instance.GetTileWithVector(new Vector2( _z , _y - 1 ));
                break;

            case ENeighbourType.RIGHT:
                return GridManager.Instance.GetTileWithVector(new Vector2(_z +1, _y));
                break;

            case ENeighbourType.LEFT:
                return GridManager.Instance.GetTileWithVector(new Vector2(_z - 1, _y));
                break;
        }

        return null;

    }

    public void PlaceExplosive()
    {
        _explosivePlaced = true;
    }
    #endregion public Methods
    #endregion Methods

}
