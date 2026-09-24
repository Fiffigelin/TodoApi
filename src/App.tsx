import { useEffect } from "react";
import "./App.css";
import { useTodos } from "./api/hooks/use-todos";
import TodoCard from "./components/todo-card";

function App() {
	const { todos, isLoading, handleLoadData, handleToggleStatus } = useTodos();

	useEffect(() => {
		handleLoadData();
	}, []);

	return (
		<div className="w-full flex justify-center">
			<div className="flex flex-col gap-6">
				{isLoading ? (
					<p>Laddar</p>
				) : (
					todos.map((t) => (
						<TodoCard
							key={t.id}
							todo={t}
							handleToggleStatus={handleToggleStatus}
						/>
					))
				)}
			</div>
		</div>
	);
}

export default App;
