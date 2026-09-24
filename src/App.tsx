import "./App.css";
import { useTodos } from "./api/hooks/use-todos";

function App() {
	const todos = useTodos();

	return (
		<div>
			{/* {todos.map((t) => (
				<div>
					<p>t.name</p>
				</div>
			))} */}
		</div>
	);
}

export default App;
