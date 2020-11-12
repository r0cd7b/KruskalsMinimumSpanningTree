using System;

namespace _20163248_이상균
{
    class Vertex<ElementType> // 정점을 표현하는 클래스.
    {
        ElementType data; // 정점의 데이터 필드를 제네릭으로 구현.
        int index; // 정점의 index 필드.
        Vertex<ElementType> next; // 정점들을 리스트 형태로 저장하기 위한 링크 필드.
        Edge<ElementType> adjacencyList; // 인접 리스트 필드.

        public ElementType Data // 프로퍼티.
        {
            get { return data; }
        }
        public int Index // 프로퍼티.
        {
            get { return index; }
            set { index = value; }
        }
        public Vertex<ElementType> Next // 프로퍼티.
        {
            get { return next; }
            set { next = value; }
        }
        public Edge<ElementType> AdjacencyList // 프로퍼티.
        {
            get { return adjacencyList; }
        }

        public Vertex(ElementType data) // 정점 생성자.
        {
            this.data = data;
            index = -1; // index의 초기 값은 -1. 그래프 클래스에 해당 정점을 추가함으로써 index에 고유 값이 할당된다.
        }

        public void Destroy() // 인접 리스트를 null로 할당.
        {
            adjacencyList = null;
        }

        public void AddEdge(Edge<ElementType> e) // 정점에 간선을 연결하는 메소드.
        {
            if (adjacencyList == null) // 인접 리스트가 비어있다면,
                adjacencyList = e; // e가 첫번째 인접 리스트가 된다.
            else
            {
                Edge<ElementType> adjacencyList = this.adjacencyList;

                while (adjacencyList.Next != null) // 다음 인접 리스트가 null일 때까지 반복한다.
                    adjacencyList = adjacencyList.Next; // 다음 인접 리스트를 현재 인접 리스트로 지정한다(최종적으로 마지막 인접 리스트가 현재 인접 리스트가 된다.).

                adjacencyList.Next = e; // 현재 인접 리스트의 다음 리스트를 e로 지정한다.
            }
        }
    }

    class Edge<ElementType> // 간선을 표현하는 클래스.
    {
        int weight; // 간선의 가중치 값.
        Edge<ElementType> next; // 간선들을 리스트 형태로 저장하기 위한 링크 필드.
        Vertex<ElementType> from; // from 정점.
        Vertex<ElementType> target; // target 정점.

        public int Weight // 프로퍼티.
        {
            get { return weight; }
        }
        public Edge<ElementType> Next // 프로퍼티.
        {
            get { return next; }
            set { next = value; }
        }
        public Vertex<ElementType> From // 프로퍼티.
        {
            get { return from; }
        }
        public Vertex<ElementType> Target // 프로퍼티.
        {
            get { return target; }
        }

        public Edge(Vertex<ElementType> from, Vertex<ElementType> target, int weight) // 간선 생성자.
        {
            this.from = from;
            this.target = target;
            this.weight = weight;
        }
    }

    partial class Graph<ElementType> // 그래프를 표현하는 클래스.
    {
        Vertex<ElementType> vertices; // 리스트 형태로 저장된 정점 중 첫번째 정점.
        int vertexCount; // 정점의 개수.

        public void Destroy() // 그래프 제거 메소드.
        {
            while (vertices != null) // 정점이 빌 때까지 반복.
            {
                vertices.Destroy(); // 현재 정점의 인접 리스트 삭제.
                vertices = vertices.Next; // 다음 정점을 현재 정점으로 지정.
            }
        }

        public void AddVertex(Vertex<ElementType> v) // 그래프에 정점을 추가하는 메소드.
        {
            Vertex<ElementType> vertexList = vertices; // vertexList를 첫번째 정점으로 지정.

            if (vertexList == null) // 현재 정점이 null일 경우,
                vertices = v; // 현재 정점에 추가할 정점(v) 할당.
            else // null이 아닐 경우.
            {
                while (vertexList.Next != null) // 다음 정점 리스트가 null이 될 때까지 반복.
                    vertexList = vertexList.Next; // 현재 정점을 다음 정점으로 지정.

                vertexList.Next = v; // 다음 정점(즉, 마지막 정점의 다음 정점)을 새로운 정점(v)로 할당.
            }

            v.Index = vertexCount++; // 해당 v의 Index 값을 vertexCount로 할당 후 vertexCount에 1을 더함.
        }

        public void Print() // 그래프를 화면에 출력하는 메소드.
        {
            Vertex<ElementType> v = null; // 출력에 사용할 임시 정점 객체 v.
            Edge<ElementType> e = null; // 출력에 사용할 임시 간선 객체 e.

            if ((v = vertices) == null) // 첫번째 정점으로 지정한 v가 null일 경우,
                return; // 출력할 값이 없으므로 리턴.

            while (v != null) // v가 null이 아닐 경우 반복.
            {
                Console.Write("{0} : ", v.Data); // 현재 정점의 data 값 출력.

                if ((e = v.AdjacencyList) == null) // 현재 정점의 인접 리스트 중 첫번째 간선으로 지정한 e가 null일 경우.
                {
                    v = v.Next; // 다음 정점을 현재 정점으로 지정.
                    Console.WriteLine();
                    continue; // 다음 반복문으로 건너뜀.
                }

                while (e != null) // e가 null이 아닐 경우.
                {
                    Console.Write("{0}[{1}] ", e.Target.Data, e.Weight); // 현재 간선 e가 가리키는 정점 target과 해당 가중치를 출력.
                    e = e.Next; // 다음 간선을 현재 간선으로 지정.
                }

                Console.WriteLine();

                v = v.Next; // 다음 정점을 현재 정점으로 지정.
            }

            Console.WriteLine();
        }
    }
}