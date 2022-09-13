import './App.css';
import TopNav from './TopNav'
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import NewsSources from './NewsSources'
import Blog from './pages/blog'

function App() {
  return (
    <div className="App">
      <Router>
        <TopNav title="Markdown Blog" />
        <br />
        <div className="content">
          <Routes>
            <Route path="/" element={<Blog />} />
            <Route path="/news" element={<NewsSources />} />
            <Route path="/blog" element={<Blog />} />
          </Routes>
        </div>
      </Router>
    </div>
  );
}

export default App;
