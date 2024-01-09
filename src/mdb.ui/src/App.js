import './App.css';
import TopNav from './components/TopNav'
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import NewsSources from './NewsSources'
import Blog from './pages/blog'
import Sample from './pages/sample/sample';
import Post from './pages/post';
import Filter from './pages/filter';
import { Helmet } from 'react-helmet';

function App() {
  return (
    <div className="App">
      <Helmet>
        <title>Hey Title</title>
        <meta name="description" content="Hey Description" />
        <meta property="og:title" content="Hey Title" />
        <meta property="og:image" content="https://picsum.photos/1000/300" />
      </Helmet>
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
  );
}

export default App;
