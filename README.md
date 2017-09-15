# Life is Great

The problem shown in the exercise is a typical sorting algorithm. The dependencies of the jobs, give an extra level of complexity, especially when circular dependencies appear.
To solve the problem, I have applied the Depth-first search algorithm, which is based in the original Kahn's algorithm (Wikipedia source):
<br />
L ← Empty list that will contain the sorted elements
<br />
S ← Set of all nodes with no incoming edge
<br />
while S is non-empty do
<br />
    remove a node n from S
	<br />
    add n to tail of L
	<br />
    for each node m with an edge e from n to m do
	<br />
        remove edge e from the graph
		<br />
        if m has no other incoming edges then
		<br />
            insert m into S
			<br />
if graph has edges then
<br />
    return error (graph has at least one cycle)
	<br />
else 
<br />
    return L (a topologically sorted order)
	<br />
<br />
<br />
The depth-first search is faster and more effective:<br />
L ← Empty list that will contain the sorted nodes<br />
while there are unmarked nodes do<br />
    select an unmarked node n<br />
    visit(n) <br />
function visit(node n)<br />
    if n has a permanent mark then return<br />
    if n has a temporary mark then stop (not a DAG)<br />
    if n is not marked (i.e. has not been visited yet) then<br />
        mark n temporarily<br />
        for each node m with an edge from n to m do<br />
            visit(m)<br />
        mark n permanently<br />
        add n to head of L<br />
<br />
<br />
<br />
Each node n gets prepended to the output list L only after considering all other nodes which depend on n (all descendants of n in the graph). Specifically, when the algorithm adds node n, we are guaranteed that all nodes which depend on n are already in the output list L: they were added to L either by the recursive call to visit() which ended before the call to visit n, or by a call to visit() which started even before the call to visit n. Since each edge and node is visited once, the algorithm runs in linear time.
To use it just work with the tests.

