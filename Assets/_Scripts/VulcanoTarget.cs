using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider))]

public class VulcanoTarget : MonoBehaviour
{
    #region Inspector properties


    #endregion

    #region Private Properties
    protected SpriteRenderer _spriteRenderer;
    protected Transform _transform;
    protected BoxCollider _boxCollider;
    //Posiciones del tablero donde hay volcanes y no deberian ser targeteables
    protected int[] OmitedTiles = { 24, 25, 33, 34 , 56, 57 ,65,66 };
    #endregion

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _transform = GetComponent<Transform>();


        if (_transform == null)
            Debug.LogError("Missing component !");

        if (_spriteRenderer == null)
            Debug.LogError("Missing component !");
    }

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Activa el target un turno antes de que caiga la bola de fuego, y lo mantiene hasta el próximo turno.
        if (GameManager.Instance.prepareRedTargets && !_spriteRenderer.enabled)
        {
            _transform.position = PrepareTarget();

        }


    }

    //Metodo para generar una posición aleatoria del mapa, omitiendo las que ocupan los volcanes
    public int GenerateRandomNumber()
    {
        int RandomNum = UnityEngine.Random.Range(0, GameManager.Instance.positions.Length);
        for (int i = 0; i< OmitedTiles.Length; i++)
        {
            if (RandomNum == OmitedTiles[i])
            {
                Debug.Log(RandomNum);
                return (GenerateRandomNumber());
            }
        }
        return RandomNum;
    }

    public Vector3 PrepareTarget()
    {
        int target = GenerateRandomNumber();
        Transform targettrans;

        targettrans = GameManager.Instance.positions[target].transform;

        _spriteRenderer.enabled = true;

        //Coloca el target en una posición aleatoria del tablero.
        return new Vector3(targettrans.position.x, targettrans.position.y + 0.5f, targettrans.position.z);
    }

    //Desactiva el target después de que cayó la bola de fuego.
     void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trowable")
        {
            _spriteRenderer.enabled = false;
        }
    }
}
