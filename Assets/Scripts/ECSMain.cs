using System.ComponentModel.Design;
using Unity.Collections;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine.UI;

public class ECSMain : MonoBehaviour
{
    [SerializeField]
    private Mesh _mesh;
    
    [SerializeField]
    private Material _material;
    
    [SerializeField]
    private Text _counter;

    [SerializeField]
    private int _createNum = 100;

    private EntityManager entityManager;
    private EntityArchetype fallCubeArch;
    
    private int _createCount = 0;
    
    void Start()
    {
        // EntityManagerを取得
        entityManager = World.Active.GetOrCreateManager<EntityManager>();

        // Entityのアーキタイプを定義
        fallCubeArch = entityManager.CreateArchetype(
            typeof(TransformMatrix),
            typeof(MeshInstanceRenderer),
            typeof(FallCubeData),
            typeof(Position),
            typeof(Rotation));

        // FirstCreate
        Create();
    }

    void Create()
    {
        // アーキタイプを元にEntityを実際に生成
        var array = new NativeArray<Entity>(_createNum, Allocator.TempJob);
        entityManager.CreateEntity(fallCubeArch, array);
        foreach (var entity in array)
        {
            entityManager.SetComponentData(entity, new FallCubeData
            {
                fallSpeed = Random.Range(1f, 10f),
                rotationSpeed = Random.Range(-50f, 50f)
            });
            entityManager.SetComponentData(entity, new Position(new float3(Random.Range(-100, 100),Random.Range(-100, 100),Random.Range(-100, 100))));
            entityManager.SetComponentData(entity,
                new Rotation(Quaternion.AngleAxis(Random.Range(1, 360), Vector3.up)));
            entityManager.SetSharedComponentData(entity, new MeshInstanceRenderer
            {
                mesh = _mesh,
                material = _material
            });
        }
        array.Dispose();

        _createCount += _createNum;
        _counter.text = _createCount.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            Create();
        }
    }
    
}