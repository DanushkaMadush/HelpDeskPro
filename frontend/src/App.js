import logo from './logo.svg';
import './App.css';
import { useTheme } from './context/ThemeContext';
import './index.css';

function App() {
  const { toggleTheme } = useTheme();

  return (
    <div>
      <h1>HelpDesk Pro</h1>
      <button onClick={toggleTheme}>Toggle Theme</button>
    </div>
  );
}

export default App;
