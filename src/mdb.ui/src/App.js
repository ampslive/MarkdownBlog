import './App.css';
import TopNav from './components/TopNav'
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom'
import NewsSources from './NewsSources'
import Blog from './pages/blog'
import Sample from './pages/sample/sample';
import Post from './pages/post';
import Filter from './pages/filter';

function App() {
  return (
    <div className="App">
      <Router>
        <TopNav title="AMIT PHILIPS" />
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
        <footer class="footer mt-auto py-3 bg-light text-center fs-6">
          <div class="container">
            <span class="fw-lighter">This site was developed on React 18, Azure Function 4, .NET 8, hosted on Azure. To know more visit the repo for <a target='_blank' href="https://github.com/ampslive/MarkdownBlog">MarkDown Blog</a></span>
          </div>
        </footer>
      </Router>
    </div>
  );
}

export default App;
