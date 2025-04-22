import { useState, useEffect } from 'react';
import Order from './Order';

const Orders = () => {
    const [orders, setOrders] = useState([]);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchOrders = async () => {
            try {
                const response = await fetch('http://localhost:5278/api/orders');
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                const json = await response.json();
                setOrders(json.data || []); // Ensure `data` exists
            } catch (err) {
                console.error('Error fetching orders:', err);
                setError(err.message);
            }
        };
        fetchOrders();
    }, []); // Add an empty dependency array to avoid infinite re-renders

    if (error) {
        return <div>Error: {error}</div>;
    }

    if (orders.length === 0) {
        return <div className='w-svw'>No orders available.</div>;
    }

    return (
        <div className='flex h-screen w-screen'>
            <aside className='flex items-center flex-col w-1/10 min-w-fit border-r p-4'>
                <button className='fixed w-fit'>Add</button>
            </aside>
            <div className='w-9/10'>
                {orders.map((order, index) => (
                    <Order order={order} key={index}/>
                ))}
            </div>
        </div>
    );
};

export default Orders;