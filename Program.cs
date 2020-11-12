using System;

namespace _20163248_이상균
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph<char> graph = new Graph<char>();
            Graph<char> kruskalMST = new Graph<char>();

            Vertex<char> a = new Vertex<char>('A');
            Vertex<char> b = new Vertex<char>('B');
            Vertex<char> c = new Vertex<char>('C');
            Vertex<char> d = new Vertex<char>('D');
            Vertex<char> e = new Vertex<char>('E');
            Vertex<char> f = new Vertex<char>('F');
            Vertex<char> g = new Vertex<char>('G');

            graph.AddVertex(a);
            graph.AddVertex(b);
            graph.AddVertex(c);
            graph.AddVertex(d);
            graph.AddVertex(e);
            graph.AddVertex(f);
            graph.AddVertex(g);

            a.AddEdge(new Edge<char>(a, b, 7));
            a.AddEdge(new Edge<char>(a, d, 5));

            b.AddEdge(new Edge<char>(b, a, 7));
            b.AddEdge(new Edge<char>(b, c, 8));
            b.AddEdge(new Edge<char>(b, d, 9));
            b.AddEdge(new Edge<char>(b, e, 7));

            c.AddEdge(new Edge<char>(c, b, 8));
            c.AddEdge(new Edge<char>(c, e, 5));

            d.AddEdge(new Edge<char>(d, a, 5));
            d.AddEdge(new Edge<char>(d, b, 9));
            d.AddEdge(new Edge<char>(d, e, 15));
            d.AddEdge(new Edge<char>(d, f, 6));

            e.AddEdge(new Edge<char>(e, b, 7));
            e.AddEdge(new Edge<char>(e, c, 5));
            e.AddEdge(new Edge<char>(e, d, 15));
            e.AddEdge(new Edge<char>(e, f, 8));
            e.AddEdge(new Edge<char>(e, g, 9));

            f.AddEdge(new Edge<char>(f, d, 6));
            f.AddEdge(new Edge<char>(f, e, 8));
            f.AddEdge(new Edge<char>(f, g, 11));

            g.AddEdge(new Edge<char>(g, e, 9));
            g.AddEdge(new Edge<char>(g, f, 11));

            Console.WriteLine("Graph");
            graph.Print();

            Console.WriteLine("Kruskal's Algorithm...");
            graph.Kruskal(kruskalMST);
            kruskalMST.Print();

            /* 이전 생략 */

            kruskalMST.Destroy();
            graph.Destroy();
        }
    }
}