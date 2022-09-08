import './App.css';
import TopNav from './TopNav'
import NewsSources from './NewsSources'
import Blog from './Blog'

function App() {
  return (
    <div className="App">
      <TopNav title="Markdown Blog" />
      <Blog />
      <hr/>
      <NewsSources />
    </div>
  );
}

export default App;
