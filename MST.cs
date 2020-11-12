using System;

namespace _20163248_이상균
{
    partial class Graph<ElementType> // 그래프를 표현하는 클래스.
    {
        public void Kruskal(Graph<ElementType> mst) // 크루스칼 알고리즘 메소드.
        {
            Vertex<ElementType>[] mstVertices = new Vertex<ElementType>[vertexCount]; // 현재 그래프의 정점 수만큼 최소 신장 트리(MST)의 정점 배열을 선언.
            DisjointSet<Vertex<ElementType>>[] vertexSet = new DisjointSet<Vertex<ElementType>>[vertexCount]; // 정점 수 만큼 분리 집합 배열을 선언.

            PriorityQueue<Edge<ElementType>> pq = new PriorityQueue<Edge<ElementType>>(10); // 우선순위 큐를 선언, 크기는 10으로 임의 지정한다.

            int i = 0; // 반복문을 수행하기 위해 i는 0으로 초기화.
            Vertex<ElementType> currentVertex = vertices; // 현재 정점을 나타내는 변수를 선언, 해당 그래프의 첫번째 정점으로 할당한다.
            while (currentVertex != null) // 현재 정점이 null이 될 때까지 반복한다.
            {
                vertexSet[i] = new DisjointSet<Vertex<ElementType>>(currentVertex); // i번째 집합 배열에 현재 정점 집합을 할당한다.
                mstVertices[i] = new Vertex<ElementType>(currentVertex.Data); // i번째 MST 정점에 현재 정점을 복사한다.
                mst.AddVertex(mstVertices[i]); // MST 그래프에 i번째 MST 정점을 추가한다.

                Edge<ElementType> currentEdge = currentVertex.AdjacencyList; // 현재 간선을 현재 정점의 첫번째 인접 리스트로 할당한다.
                while (currentEdge != null) // 현재 간선이 null이 될 때까지 반복.
                {
                    PQNode<Edge<ElementType>> newNode = new PQNode<Edge<ElementType>>(currentEdge.Weight, currentEdge); // 현재 간선의 가중치를 우선순위 값으로, 현재 간선 객체를 데이터로, 우선순위 큐 노드를 생성한다.
                    pq.Enqueue(newNode); // 생성한 노드를 큐에 Enqueue한다.

                    currentEdge = currentEdge.Next; // 다음 간선을 현재 간선으로 지정한다.
                }

                currentVertex = currentVertex.Next; // 다음 정점을 현재 정점으로 지정한다.
                i++; // i 값을 1 증가시킨다.
            }

            while (!pq.IsEmpty()) // 큐가 빌 때까지 반복.
            {
                pq.Dequeue(out PQNode<Edge<ElementType>> popped); // Dequeue한 큐의 노드를 popped 노드에 복사한다.
                Edge<ElementType> currentEdge = popped.Data; // popped 노드 데이터(간선)를 currentEdge에 할당한다.
                Console.WriteLine("{0} - {1} : {2}", currentEdge.From.Data, currentEdge.Target.Data, currentEdge.Weight);

                int fromIndex = currentEdge.From.Index; // 현재 간선 정보 중 시발 정점의 Index 값을 fromIndex에 할당한다.
                int toIndex = currentEdge.Target.Index; // 현재 간선 정보 중 종발 정점의 Index 값을 toIndex에 할당한다.

                if (DisjointSet<Vertex<ElementType>>.FindSet(vertexSet[fromIndex]) != DisjointSet<Vertex<ElementType>>.FindSet(vertexSet[toIndex])) // 시발 정점의 집합과 종발 정점의 집합이 다를 경우,
                {
                    mstVertices[fromIndex].AddEdge(new Edge<ElementType>(mstVertices[fromIndex], mstVertices[toIndex], currentEdge.Weight)); // MST 그래프에서 시발 정점의 인접리스트에 현재 간선을 추가한다.

                    mstVertices[toIndex].AddEdge(new Edge<ElementType>(mstVertices[toIndex], mstVertices[fromIndex], currentEdge.Weight)); // MST 그래프에서 종발 정점의 인접리스트에 현재 간선을 추가한다.

                    vertexSet[fromIndex].UnionSet(vertexSet[toIndex]); // 시발 정점의 집합에 종발 정접의 집합을 합한다.
                }
            }
        }
    }
}