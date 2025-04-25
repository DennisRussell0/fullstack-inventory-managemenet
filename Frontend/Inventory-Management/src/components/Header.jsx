import { NavLink } from 'react-router-dom';

const Header = () => {
    return (
        <header className=' border-b p-4 sticky top-0 h-16 z-10'>
            <nav className='flex justify-evenly text-xl font-semibold' id="nav">
                <NavLink 
                    to="/storage"
                    className={({ isActive }) =>
                        isActive ? "text-[#646cff]" : "font-normal"
                    }
                >
                    Storage
                </NavLink>
                <NavLink 
                    to="/orders"
                    className={({ isActive }) =>
                        isActive ? "text-[#646cff]" : "font-normal"
                    }
                >
                    Orders
                </NavLink>
            </nav>
        </header>
    );
};

export default Header;