import './App.css';
import TopNav from './components/TopNav'
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import NewsSources from './NewsSources'
import Blog from './pages/blog'
import Sample from './pages/sample/sample';
import Post from './pages/post';
import Filter from './pages/filter';

function App() {
  return (
    <div className="App">
      <Router>
        <TopNav title="Markdown Blog" />
        <div className="content">
          <Routes>
            <Route path="/" element={<Blog />} />
            <Route path="/news" element={<NewsSources />} />
            <Route path="/blog" element={<Blog />} />
            <Route path="/blog/series/:searchSeries" element={<Filter />} />
            <Route path="/sample" element={<Sample />} />
            <Route path="/post/:id/:title" element={<Post />} />
          </Routes>
        </div>
      </Router>
    </div>
  );
}

export default App;
