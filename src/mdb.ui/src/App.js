import './App.css';
import TopNav from './TopNav'
import NewsSources from './NewsSources'

function App() {
  return (
    <div className="App">
      <TopNav title="Markdown Blog" />
      <NewsSources />
    </div>
  );
}

export default App;
