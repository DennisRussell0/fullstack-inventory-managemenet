import { useState, useEffect } from 'react'
import ProductCard from './ProductCard';

const Storage = () => {

    const [products, setProducts] = useState([]);

    useEffect(() => {
        const fetchProducts = async () => {
            try {
                const response = await fetch('http://localhost:5278/api/products');
                const json = await response.json();
                const data = json.data;
                setProducts(data);
            } catch (error) {
                console.error('Error fetching products:', error);
            }
        };
        fetchProducts();
    })

    return (
        <div className='flex h-full w-screenl'>
            <aside className='flex items-center flex-col w-1/10 min-w-fit border-r p-4'>
                <button className='fixed w-fit'>Add</button>
            </aside>
            <div id="productCardWrapper" className="w-9/10 flex gap-8 flex-wrap justify-center py-8 px-4">
                {products.map((product, i) => (
                    <ProductCard key={i} product={product} />
                ))}
            </div>
        </div>
    )
}

export default Storage;