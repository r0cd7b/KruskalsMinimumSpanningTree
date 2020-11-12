using System;

namespace _20163248_이상균
{
    class PQNode<ElementType> // 큐의 노드 클래스.
    {
        int priority; // 우선순위 필드. 해당 필드의 값이 작을수록 우선순위가 높다.
        ElementType data; // 데이터 필드를 제네릭으로 구현.

        public int Priority // 프로퍼티.
        {
            get { return priority; }
        }
        public ElementType Data // 프로퍼티.
        {
            get { return data; }
        }

        public PQNode(PQNode<ElementType> pqNode) // 복사 생성자.
        {
            priority = pqNode.priority;
            data = pqNode.data;
        }
        public PQNode(int priority, ElementType data) // 생성자.
        {
            this.priority = priority;
            this.data = data;
        }
    }

    class PriorityQueue<ElementType> // 우선순위 큐 클래스.
    {
        PQNode<ElementType>[] nodes; // 큐의 노드 객체 배열.
        int capacity; // 큐의 용량을 나타내는 capacity 필드.
        int usedSize; // 큐의 사용량을 나타내는 usedSize 필드(마지막 노드의 위치 + 1이다.).

        public int UsedSize // 프로퍼티.
        {
            get { return usedSize; }
        }

        public PriorityQueue(int initialSize) // 생성자. initialSize 인자의 크기만큼 nodes 배열 생성.
        {
            capacity = initialSize;
            nodes = new PQNode<ElementType>[capacity];
        }

        public void Enqueue(PQNode<ElementType> newNode) // 노드 삽입 메소드.
        {
            int currentPosition = usedSize; // currentPosition을 usedSize 위치(마지막 노드의 위치 + 1)로 지정.
            int parentPosition = GetParent(currentPosition); // parentPosition을 currentPosition의 부모 위치로 지정.

            if (usedSize == capacity) // 만약 큐가 가득 찼을 경우.
            {
                if (capacity == 0) // 용량이 0인 경우,
                    capacity = 1; // 용량을 1로 늘린다.

                capacity *= 2; // 용량을 2배로 늘린다.
                Array.Resize(ref nodes, capacity); // 바뀐 크기에 맞게 배열을 재할당.
            }

            nodes[currentPosition] = new PQNode<ElementType>(newNode); // 현재 위치의 노드에 삽입할 노드(인자)를 복사한다.

            while (currentPosition > 0 && nodes[currentPosition].Priority < nodes[parentPosition].Priority) // 현재 위치가 0보다 크면서(위치가 0일 경우 깊이가 0이면서 우선순위가 가장 높은 노드이다.) 우선순위가 부모 노드보다 높을 경우,
            { // 높은 우선 순위에 따라 힙 정렬을 수행하여 트리를 재구성한다.
                SwapNodes(currentPosition, parentPosition); // 현재 노드와 부모 노드의 위치를 맞바꾼다.

                currentPosition = parentPosition; // 현재 위치를 부모의 위치로 옮김.
                parentPosition = GetParent(currentPosition); // 현재 위치를 기준으로 다시 부모 위치를 구하여 지정한다.
            }

            usedSize++; // 노드가 하나 추가되었으므로 사용량을 1 늘린다.
        }

        public void SwapNodes(int index1, int index2) // 두 노드의 위치를 맞바꾸는 메소드.
        {
            PQNode<ElementType> temp = new PQNode<ElementType>(nodes[index1]); // 임시 객체에 index1을 복사한다.
            nodes[index1] = new PQNode<ElementType>(nodes[index2]); // index1에 index2를 복사한다.
            nodes[index2] = new PQNode<ElementType>(temp); // index2에 임시 객체를 복사한다.
        }

        public void Dequeue(out PQNode<ElementType> root) // 노드 삭제 메소드.
        {
            int parentPosition = 0; // 부모 노드의 위치 변수 선언.

            root = new PQNode<ElementType>(nodes[0]); // 우선 순위가 가장 높은 노드(priority 값이 낮은 노드)를 root에 복사하여 반환한다.
            nodes[0] = new PQNode<ElementType>(default(int), default(ElementType)); // 삭제된 노드의 모든 필드를 기본값으로 초기화한다. (사용하지 않는 노드이므로 필드 값을 비워줌.)

            usedSize--; // usedSize를 1 낮춘다(이로써 usedSize는 마지막 노드의 위치 값이다.).
            SwapNodes(0, usedSize); // 사용하지 않는 첫번째 노드(인덱스 0)를 마지막 노드와 맞바꾼다.

            int leftPosition = GetLeftChild(0); // 바뀐 첫번째 노드(마지막에 있던 노드)의 왼쪽 자식 위치를 leftPosition으로 할당한다.
            int rightPosition = leftPosition + 1; // leftPosition + 1(오른쪽 자식 노드 위치)의 값을 rightPosition로 할당한다.

            while (true) // 무한 루프.
            { // 높은 우선 순위에 따라 힙 정렬을 수행하여 트리를 재구성한다.
                int selectedChild = 0; // selectedChild(기준 노드의 위치) 변수를 선언한다.

                if (leftPosition >= usedSize) // 왼쪽 자식 노드가 사용 범위 밖일 경우(usedSize와 같거나 보다 클 경우),
                    break; // 루프를 빠져나간다.

                if (rightPosition >= usedSize) // 왼쪽 자식 노드가 사용 범위 안이면서 오른쪽 자식 노드는 밖일 경우,
                    selectedChild = leftPosition; // 왼쪽 자식의 위치를 selectedChild로 할당한다.
                else // 오른쪽 자식 노드도 사용 범위 안일 경우,
                {
                    if (nodes[leftPosition].Priority > nodes[rightPosition].Priority) // 왼쪽 자식 노드의 우선순위가 오른쪽 자식 노드보다 낮은 경우,
                        selectedChild = rightPosition; // 오른쪽 자식 노드의 위치를 selectedChild로 할당한다.
                    else // 왼쪽 자식 노드의 우선 순위가 오른쪽 자신 노드와 같거나 보다 높을 경우,
                        selectedChild = leftPosition; // 왼쪽 자식 노드의 위치를 selectedChild로 할당한다.
                }

                if (nodes[selectedChild].Priority < nodes[parentPosition].Priority) // selectedChild 위치의 우선순위가 부모(처음 parentPosition의 값은 0이다.) 노드보다 높을 경우.
                {
                    SwapNodes(parentPosition, selectedChild); // selectedChild 위치의 노드와 그 부모 노드를 맞바꾼다.
                    parentPosition = selectedChild; // 부모 노드의 위치를 selectedChild 위치로 바꾼다.
                }
                else // selectedChild 위치의 우선순위가 부모 노드와 같거나 보다 낮을 경우,
                    break; // 루프를 빠져나간다.

                leftPosition = GetLeftChild(parentPosition); // 바뀐 부모 노드의 왼쪽 자식 노드 위치를 leftPosition에 할당한다.
                rightPosition = leftPosition + 1; // leftPosition + 1(오른쪽 자식 노드 위치)의 값을 rightPosition로 할당한다.
            }
        }

        public int GetParent(int index) // 부모 위치를 구하는 메소드.
        {
            return (index - 1) / 2; // 부모 위치(기준 위치에서 1을 빼고 2로 나눈 값.)을 반환한다.
        }

        public int GetLeftChild(int index) // 왼쪽 자식 위치를 구하는 메소드.
        {
            return 2 * index + 1; // 왼쪽 자식 위치를 반환한다(기준 위치에서 2를 곱하고 1을 더한 것이 왼쪽 자식 위치이다.).
        }

        public bool IsEmpty() // 큐가 비어 있는지 검사하는 메소드.
        {
            return usedSize == 0; // 사용량이 0이면 true를 반환한다.
        }
    }
}