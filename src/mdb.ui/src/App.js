import './App.css';
import TopNav from './components/TopNav'
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import NewsSources from './NewsSources'
import Blog from './pages/blog'
import Sample from './pages/sample/sample';
import Post from './pages/post';
import Filter from './pages/filter';
import { HelmetProvider } from 'react-helmet-async';

function App() {
  return (
    <HelmetProvider>
      <div className="App">
        <Router>
          <TopNav title="Markdown Blog" />
          <div className="content">
            <Routes>
              <Route path="/" element={<Blog />} />
              <Route path="/news" element={<NewsSources />} />
              <Route path="/blog" element={<Blog />} />
              <Route path="/blog/:filter/:searchTerm" element={<Filter />} />
              <Route path="/sample" element={<Sample />} />
              <Route path="/post/:id/:title" element={<Post />} />
            </Routes>
          </div>
        </Router>
      </div>
    </HelmetProvider>
  );
}

export default App;
