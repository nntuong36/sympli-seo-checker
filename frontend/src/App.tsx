import { ToastContainer } from 'react-toastify';
import './App.css';
import SearchRankingPage from './pages/search-ranking-page';

function App() {
  return (
    <div className="App">
      <SearchRankingPage
        pageTitle='Search Ranking' 
      />
      <ToastContainer />
    </div>
  );
}

export default App;
