namespace _20163248_이상균
{
    class DisjointSet<ElementType> // 분리 집합 클래스.
    {
        public DisjointSet<ElementType> parent; // 해당 집합의 부모 필드.
        ElementType data; // 해당 집합의 데이터 필드.

        public DisjointSet(ElementType newData) { data = newData; } // 분리 집합 생성자.

        public void UnionSet(DisjointSet<ElementType> set) // 합집합 연산.
        {
            set = FindSet(set); // 집합 set을 자신의 부모(루트)로 지정한다.
            set.parent = this; // 현재 집합을 집합 set의 부모로 할당한다. 최종적으로 set은 this의 합집합이 되었다.
        }

        public static DisjointSet<ElementType> FindSet(DisjointSet<ElementType> set) // 집합 탐색 연산.
        {
            while (set.parent != null) // 집합 set의 부모가 null이 될 때까지 반복,
                set = set.parent; // 집합 set을 자신의 부모로 지정한다.

            return set; // 집합 set 반환. 최종적으로 set의 부모(루트)가 반환된다.
        }
    }
}