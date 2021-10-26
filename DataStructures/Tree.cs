using Xunit;

namespace LeetCode.DataStructures
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int x) { val = x; }
        public static TreeNode Create(IEnumerable<int?> values, TraversalType ttype = TraversalType.LevelOrder)
            => TreeNodeHelper.Dispatch<TreeNode>(nameof(Create), ttype, values);
        public static TreeNode Create(int? val) => val == null ? null : new TreeNode(val.Value);
    }

    public enum TraversalType
    {
        PreOrder, // DLR先序
        InOrder, // LDR中序
        PostOrder, // LRD后序
        LevelOrder // 层次遍历
    }

    public static class TreeNodeHelper
    {
        // 获得CreateByLevelOrder等函数
        internal static System.Reflection.MethodInfo GetFunction(string action, TraversalType ttype) =>
            typeof(TreeNodeHelper).GetMethod(action + "By" + ttype.ToString(),
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        // 调用函数获得结果
        internal static T Dispatch<T>(string action, TraversalType ttype, params object[] args) =>
            (T)GetFunction(action, ttype).Invoke(null, args);

        internal static TreeNode CreateByLevelOrder(IEnumerable<int?> values)
        {
            var e = values.GetEnumerator();
            if (e.MoveNext() == false)
                return null; // []也要返回null而不是抛异常

            var q = new Queue<TreeNode>(); // 可以含有null
            var root = TreeNode.Create(e.Current);
            q.Enqueue(root);

            while (e.MoveNext() != false)
            {
                var c = q.Dequeue();

                if (c == null) // 为null的结点，左右孩子必须也为null
                    if (e.Current != null || e.MoveNext() && e.Current != null)
                        throw new ArrayTypeMismatchException("试图给不存在的结点添加孩子");
                    else
                        continue;

                c.left = TreeNode.Create(e.Current);

                if (e.MoveNext() == false) // 允许最后为null的没有
                    break;
                c.right = TreeNode.Create(e.Current);

                q.Enqueue(c.left); // 即使为null也要入队
                q.Enqueue(c.right);
            }

            return root;
        }
        // 与先序遍历有一些不同，必须有一个参数用于判断左子树是否已经处理过了；序列中所有叶子结点后都要跟两个null
        internal static TreeNode CreateByPreOrder(IEnumerable<int?> values)
        {
            var e = values.GetEnumerator();
            if (e.MoveNext() == false)
                return null;

            var s = new Stack<(TreeNode, bool)>(); // 不能有为null的，因为序列里没有层次遍历中那些父结点为null但仍存在序列里的元素
            var root = TreeNode.Create(e.Current);
            s.Push((root, false));

            while (e.MoveNext() != false)
            {
                var (c, visited) = s.Pop();
                var node = TreeNode.Create(e.Current);

                if (!visited)
                {
                    s.Push((c, true));
                    if ((c.left = node) != null)
                        s.Push((c.left, false));
                }
                else if ((c.right = node) != null)
                    s.Push((c.right, false));
            }

            return root;
        }
        internal static TreeNode CreateByInOrder(IEnumerable<int?> values)
            => throw new NotImplementedException();
        internal static TreeNode CreateByPostOrder(IEnumerable<int?> values)
            => throw new NotImplementedException();

        public static IEnumerable<int?> AsEnumerable(this TreeNode tn, TraversalType ttype = TraversalType.LevelOrder) =>
            Dispatch<IEnumerable<int?>>(nameof(AsEnumerable), ttype, tn);
        public static IEnumerable<int> AsEnumerableNoNull(this TreeNode tn, TraversalType ttype = TraversalType.LevelOrder)
        {
            foreach (var node in tn.AsEnumerable(ttype))
                if (node != null)
                    yield return node.Value;
        }

        // 基本上就是书上的做法，只是为了返回null，循环条件改了一下
        internal static IEnumerable<int?> AsEnumerableByPreOrder(TreeNode tn)
        {
            var s = new Stack<TreeNode>(); // 可以含有null
            while (true) // 书上这里是s.Count!=0 || tn != null，具体可看104题的第二个解法
            {
                yield return tn?.val; // 此处如果tn不为null时才return，就是纯的先序序列
                if (tn != null)
                {
                    s.Push(tn); // 压栈自己，把当前设为左边
                    tn = tn.left;
                }
                else
                {
                    if (s.Count == 0) // 当前为null**且**栈为空时结束
                        break;
                    tn = s.Pop().right; // 别忘了往右边走
                }
            }
        }

        // 只评估自己的做法。因为每次循环的当前来源是Pop出来的，必须立即消费，否则就留不下来了，因此只能用于先序遍历
        internal static IEnumerable<int?> AsEnumerableByPreOrder2(TreeNode tn)
        {
            var s = new Stack<TreeNode>();
            s.Push(tn);
            while (s.Count != 0)
            {
                tn = s.Pop();
                yield return tn?.val;
                if (tn != null)
                {
                    s.Push(tn.right);
                    s.Push(tn.left);
                }
            }
        }

        internal static IEnumerable<int?> AsEnumerableByInOrder(TreeNode tn)
        {
            var s = new Stack<TreeNode>();
            while (true)
            {
                if (tn != null)
                {
                    s.Push(tn);
                    tn = tn.left;
                }
                else
                {
                    yield return null;
                    if (s.Count == 0)
                        break;
                    tn = s.Pop();
                    yield return tn.val;
                    tn = tn.right;
                }
            }
        }

        internal static IEnumerable<int?> AsEnumerableByPostOrder(TreeNode tn)
            => throw new NotImplementedException();

        // 层次遍历。cleanq的作用是消除掉最后的null，只有遇到非null的东西才会一次返回之前储存的null；算深度见104题第三个解法
        internal static IEnumerable<int?> AsEnumerableByLevelOrder(TreeNode tn)
        {
            if (tn == null) // 普通的遍历也需要处理根为null的情况，但如果是直接输出或返回字符串就不用了
                yield return null;

            var q = new Queue<TreeNode>();
            var cleanq = new Queue<int?>(); // 普通的遍历不需要这句
            q.Enqueue(tn);

            while (q.Count != 0)
            {
                var c = q.Dequeue();

                // 普通的遍历不需要几这句，直接yield return就行；要保证q队列里没有null，因此要处根为null的情况
                cleanq.Enqueue(c?.val);
                if (c != null)
                    while (cleanq.Count != 0)
                        yield return cleanq.Dequeue();

                if (c != null)
                {
                    q.Enqueue(c.left);
                    q.Enqueue(c.right);
                }
            }
        }

        // 单个结点是否相等
        public static bool Equals(TreeNode p, TreeNode q) =>
            p == null && q == null ? true :
            p == null || q == null ? false :
            p.val == q.val;
    }
}

namespace LeetCode.DataStructures.Test
{
    public class TreeNodeTest
    {
        [Theory]
        [MemberData(nameof(CreateTreeTest1Data))]
        public void CreateTreeTest1(TraversalType ttype, int?[] data)
        {
            /*  3
               / \
              9  20
                /  \
               15   7 */
            var tree = TreeNode.Create(data, ttype);
            // 按照先序断言
            Assert.Equal(3, tree.val);
            Assert.Equal(9, tree.left.val);
            Assert.Null(tree.left.left);
            Assert.Null(tree.left.right);
            Assert.Equal(20, tree.right.val);
            Assert.Equal(15, tree.right.left.val);
            Assert.Null(tree.right.left.left);
            Assert.Null(tree.right.left.right);
            Assert.Equal(7, tree.right.right.val);
            Assert.Null(tree.right.right.left);
            Assert.Null(tree.right.right.right);
        }
        public static IEnumerable<object[]> CreateTreeTest1Data()
        {
            yield return new object[] { TraversalType.LevelOrder, new int?[] { 3, 9, 20, null, null, 15, 7 } };
            yield return new object[] { TraversalType.PreOrder, new int?[] { 3, 9, null, null, 20, 15, null, null, 7 } };
        }

        [Theory]
        [MemberData(nameof(CreateTreeTest2Data))]
        public void CreateTest2(TraversalType ttype, int?[] data)
        {
            /*     1
                  / \
                 2   2
                / \
               3   3
              / \
             4   4        */
            var tree = TreeNode.Create(data, ttype);
            Assert.Equal(1, tree.val);
            Assert.Equal(2, tree.left.val);
            Assert.Equal(3, tree.left.left.val);
            Assert.Equal(4, tree.left.left.left.val);
            Assert.Null(tree.left.left.left.left);
            Assert.Null(tree.left.left.left.right);
            Assert.Equal(4, tree.left.left.right.val);
            Assert.Null(tree.left.left.right.left);
            Assert.Null(tree.left.left.right.right);
            Assert.Equal(3, tree.left.right.val);
            Assert.Null(tree.left.right.right);
            Assert.Null(tree.left.right.left);
            Assert.Equal(2, tree.right.val);
            Assert.Null(tree.right.left);
            Assert.Null(tree.right.right);
        }
        public static IEnumerable<object[]> CreateTreeTest2Data()
        {
            yield return new object[] { TraversalType.LevelOrder, new int?[] { 1, 2, 2, 3, 3, null, null, 4, 4 } };
            yield return new object[] { TraversalType.PreOrder, new int?[] { 1, 2, 3, 4, null, null, 4, null, null, 3, null, null, 2 } };
        }

        [Fact]
        public void CreateTest()
        {
            var tree = TreeNode.Create(new int?[] { null });
            Assert.Null(tree);
        }

        [Theory]
        [InlineData(TraversalType.PreOrder)]
        [InlineData(TraversalType.InOrder)]
        [InlineData(TraversalType.PostOrder)]
        [InlineData(TraversalType.LevelOrder)]
        public void ReflectionTest(TraversalType ttype)
        {
            var f1 = TreeNodeHelper.GetFunction("Create", ttype);
            Func<IEnumerable<int?>, TreeNode> f2 = ttype switch
            {
                TraversalType.PreOrder => TreeNodeHelper.CreateByPreOrder,
                TraversalType.InOrder => TreeNodeHelper.CreateByInOrder,
                TraversalType.PostOrder => TreeNodeHelper.CreateByPostOrder,
                TraversalType.LevelOrder => TreeNodeHelper.CreateByLevelOrder,
                _ => throw new NotImplementedException()
            };
            Assert.Equal(f2.Method, f1);
        }

        // [Theory]
        // [MemberData(nameof(CreateTreeTest2Data))]
        [Fact]
        public static void AsEnumerableTest()
        {
            var tn = TreeNode.Create(new int?[] { 3, 9, 20, null, null, 15, 7 });
            // var result = tn.AsEnumerable();
            // var expect = new int?[] { 3, 9, null, null, 20, 15, null, null, 7, null, null };
            var result = tn.AsEnumerable(TraversalType.LevelOrder);
            var expect = new int?[] { 3, 9, 20, null, null, 15, 7 };
            Assert.Equal(expect, result);
        }
        // 不同的树（创建方式可以只选一种，上面测试过了），选择不同的序列化方式，得到对应的结果：（树的序列，序列化方式，结果）；太多了，暂时不测试
        // public static IEnumerable<object[]> AsEnumerableTestData()
        // {
        //     yield return new object[] { TraversalType.LevelOrder, new int?[] { 1, 2, 2, 3, 3, null, null, 4, 4 } };
        //     yield return new object[] { TraversalType.PreOrder, new int?[] { 1, 2, 3, 4, null, null, 4, null, null, 3, null, null, 2 } };
        // }
    }
}
