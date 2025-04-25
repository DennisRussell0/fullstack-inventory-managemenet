import { useState, useEffect } from 'react'
import ProductCard from './ProductCard';

const Storage = () => {

    const [products, setProducts] = useState([]);
    const [filteredProducts, setFilteredProducts] = useState([]);
    const [filters, setFilters] = useState({});

    useEffect(() => {
        const fetchProducts = async () => {
            try {
                const response = await fetch('http://localhost:5278/api/products');
                const json = await response.json();
                const data = json.data;
                setProducts(data);
                setFilteredProducts(data);
            } catch (error) {
                console.error('Error fetching products:', error);
            }
        };
        fetchProducts();
    }, [])

    // Function to handle filter changes
    const handleFilterChange = (key, value) => {
        setFilters(prevFilters => ({
            ...prevFilters,
            [key]: value // Dynamically update the filter key-value pair
        }));
    };

    const handleKeyDown = (event) => {
        if (event.key === 'Enter') {
            applyFilters(); // Apply filters on Enter key press
        }
    }

    // Function to apply filters
    const applyFilters = () => {
        let filtered = products;

        // Apply each filter dynamically
        Object.entries(filters).forEach(([key, value]) => {
            if (value) {
                if (key === 'name'){filtered = filtered.filter(product => product.Name.toLowerCase().includes(value.toLowerCase()));}
                if (key === 'priceMin') {filtered = filtered.filter(product => product.Price >= parseFloat(value));}
                if (key === 'priceMax') {filtered = filtered.filter(product => product.Price <= parseFloat(value));}
                if (key === 'caloriesMin') {filtered = filtered.filter(product => product.Calories >= parseFloat(value));}
                if (key === 'caloriesMax') {filtered = filtered.filter(product => product.Calories <= parseFloat(value));}
                if (key === 'proteinMin') {filtered = filtered.filter(product => product.Protein >= parseFloat(value));}
                if (key === 'proteinMax') {filtered = filtered.filter(product => product.Protein <= parseFloat(value));}
                if (key === 'carbsMin') {filtered = filtered.filter(product => product.Carbs >= parseFloat(value));}
                if (key === 'carbsMax') {filtered = filtered.filter(product => product.Carbs <= parseFloat(value));}
                if (key === 'fatMin') {filtered = filtered.filter(product => product.Fat >= parseFloat(value));}
                if (key === 'fatMax') {filtered = filtered.filter(product => product.Fat <= parseFloat(value));}
                if (key === 'storage') {filtered = filtered.filter(product => product.Storage > 0  === (value === true));}
                if (key === 'manufacturer') {filtered = filtered.filter(product => product.Manufacturer.toLowerCase().includes(value.toLowerCase()));}
                if (key === 'sodiumMin') {filtered = filtered.filter(product => product.Sodium >= parseFloat(value));}
                if (key === 'sodiumMax') {filtered = filtered.filter(product => product.Sodium <= parseFloat(value));}
                if (key === 'fiberMin') {filtered = filtered.filter(product => product.Fiber >= parseFloat(value));}
                if (key === 'fiberMax') {filtered = filtered.filter(product => product.Fiber <= parseFloat(value));}
                if (key === 'sugarsMin') {filtered = filtered.filter(product => product.Sugars >= parseFloat(value));}
                if (key === 'sugarsMax') {filtered = filtered.filter(product => product.Sugars <= parseFloat(value));}
                if (key === 'potassiumMin') {filtered = filtered.filter(product => product.Potassium >= parseFloat(value));}
                if (key === 'potassiumMax') {filtered = filtered.filter(product => product.Potassium <= parseFloat(value));}
                if (key === 'vitaminsMin') {filtered = filtered.filter(product => product.Vitamins >= parseFloat(value));}
                if (key === 'vitaminsMax') {filtered = filtered.filter(product => product.Vitamins <= parseFloat(value));}
                if (key === 'type') {filtered = filtered.filter(product => product.Type.toLowerCase() === value.toLowerCase());}
                // Add more conditions for other filters as needed
            }
        });

        setFilteredProducts(filtered);
    };


    return (
        <div className='flex h-full w-screen'>
            <aside className='flex items-center flex-col w-2/10 border-r p-4 overflow-y-auto h-full'>
                <div className="w-full">
                    <label className="block text-gray-700 font-bold mt-4 mb-2">Product Name</label>
                    <input 
                    type="text" 
                    placeholder="Name" 
                    className="w-full border-2 border-gray-300 rounded-md p-2" 
                    onChange={(e) => handleFilterChange('name', e.target.value)} 
                    onKeyDown={handleKeyDown}/>
                </div>
                <div className="w-full">
                    <label className="block text-gray-700 font-bold mt-4 mb-2">Type</label>
                    <select defaultValue=""
                    className='w-full border-2 border-gray-300 rounded-md p-2' 
                    onChange={(e) => handleFilterChange('type', e.target.value)}>
                    <option value="">Both</option>
                    <option value="hot">Hot</option>
                    <option value="cold">Cold</option>
                    </select>
                </div>
                <div className="w-full">
                    <label className="block text-gray-700 font-bold mt-4 mb-2">Price</label>    
                    <div className="flex gap-2">
                    <input 
                        type="number" 
                        placeholder="Min" 
                        className='w-1/2 border-2 border-gray-300 rounded-md p-2' 
                        onChange={(e) => handleFilterChange('priceMin', e.target.value)}
                        onKeyDown={handleKeyDown}/>
                    <input 
                        type="number" 
                        placeholder="Max" 
                        className='w-1/2 border-2 border-gray-300 rounded-md p-2' 
                        onChange={(e) => handleFilterChange('priceMax', e.target.value)} 
                        onKeyDown={handleKeyDown}/>
                    </div>
                </div>
                <div className="w-full">
                    <label className="block text-gray-700 font-bold mt-4 mb-2">Calories</label>    
                    <div className="flex gap-2">
                    <input 
                        type="number" 
                        placeholder="Min" 
                        className='w-1/2 border-2 border-gray-300 rounded-md p-2' 
                        onChange={(e) => handleFilterChange('caloriesMin', e.target.value)}
                        onKeyDown={handleKeyDown}/>
                    <input 
                        type="number" 
                        placeholder="Max" 
                        className='w-1/2 border-2 border-gray-300 rounded-md p-2' 
                        onChange={(e) => handleFilterChange('caloriesMax', e.target.value)} 
                        onKeyDown={handleKeyDown}/></div>
                </div>
                <div className="w-full">
                    <label className="block text-gray-700 font-bold mt-4 mb-2">Protein</label>
                    <div className="flex gap-2">
                    <input
                        type="number" 
                        placeholder="Min" 
                        className='w-1/2 border-2 border-gray-300 rounded-md p-2' 
                        onChange={(e) => handleFilterChange('proteinMin', e.target.value)} 
                        onKeyDown={handleKeyDown}/>
                    <input
                        type="number" 
                        placeholder="Max" 
                        className='w-1/2 border-2 border-gray-300 rounded-md p-2' 
                        onChange={(e) => handleFilterChange('proteinMax', e.target.value)} 
                        onKeyDown={handleKeyDown}/></div>
                </div>
                <div className="w-full">
                    <label className="block text-gray-700 font-bold mt-4 mb-2">Carbs</label>
                    <div className="flex gap-2">
                    <input
                        type="number" 
                        placeholder="Min" 
                        className='w-1/2 border-2 border-gray-300 rounded-md p-2' 
                        onChange={(e) => handleFilterChange('carbsMin', e.target.value)} 
                        onKeyDown={handleKeyDown}/>
                    <input
                        type="number" 
                        placeholder="Max" 
                        className='w-1/2 border-2 border-gray-300 rounded-md p-2' 
                        onChange={(e) => handleFilterChange('carbsMax', e.target.value)} 
                        onKeyDown={handleKeyDown}/></div>
                </div>
                <div className="w-full">
                    <label className="block text-gray-700 font-bold mt-4 mb-2">Fat</label>
                        
                    <div className="flex gap-2"><input
                        type="number" 
                        placeholder="Min" 
                        className='w-1/2 border-2 border-gray-300 rounded-md p-2' 
                        onChange={(e) => handleFilterChange('fatMin', e.target.value)} 
                        onKeyDown={handleKeyDown}/>
                    <input
                        type="number" 
                        placeholder="Max" 
                        className='w-1/2 border-2 border-gray-300 rounded-md p-2' 
                        onChange={(e) => handleFilterChange('fatMax', e.target.value)} 
                        onKeyDown={handleKeyDown}/></div>
                </div>
                <div className="w-full">
                    <label className="block text-gray-700 font-bold mt-4 mb-2">Sodium</label>
                    <div className="flex gap-2"><input
                        type="number" 
                        placeholder="Min" 
                        className='w-1/2 border-2 border-gray-300 rounded-md p-2' 
                        onChange={(e) => handleFilterChange('sodiumMin', e.target.value)} 
                        onKeyDown={handleKeyDown}/>
                    <input
                        type="number" 
                        placeholder="Max" 
                        className='w-1/2 border-2 border-gray-300 rounded-md p-2' 
                        onChange={(e) => handleFilterChange('sodiumMax', e.target.value)} 
                        onKeyDown={handleKeyDown}/></div>
                </div>
                <div className="w-full">
                    <label className="block text-gray-700 font-bold mt-4 mb-2">Fiber</label>
                    <div className="flex gap-2"><input
                        type="number" 
                        placeholder="Min" 
                        className='w-1/2 border-2 border-gray-300 rounded-md p-2' 
                        onChange={(e) => handleFilterChange('fiberMin', e.target.value)} 
                        onKeyDown={handleKeyDown}/>
                    <input
                        type="number" 
                        placeholder="Max" 
                        className='w-1/2 border-2 border-gray-300 rounded-md p-2' 
                        onChange={(e) => handleFilterChange('fiberMax', e.target.value)} 
                        onKeyDown={handleKeyDown}/></div>
                </div>
                <div className="w-full">
                    <label className="block text-gray-700 font-bold mt-4 mb-2">Sugar</label>
                    <div className="flex gap-2"><input
                        type="number" 
                        placeholder="Min" 
                        className='w-1/2 border-2 border-gray-300 rounded-md p-2' 
                        onChange={(e) => handleFilterChange('sugarsMin', e.target.value)} 
                        onKeyDown={handleKeyDown}/>
                    <input
                        type="number" 
                        placeholder="Max" 
                        className='w-1/2 border-2 border-gray-300 rounded-md p-2' 
                        onChange={(e) => handleFilterChange('sugarsMax', e.target.value)} 
                        onKeyDown={handleKeyDown}/></div>
                </div>
                <div className="w-full">
                    <label className="block text-gray-700 font-bold mt-4 mb-2">Potassium</label>
                    <div className="flex gap-2"><input
                        type="number" 
                        placeholder="Min" 
                        className='w-1/2 border-2 border-gray-300 rounded-md p-2' 
                        onChange={(e) => handleFilterChange('potassiumMin', e.target.value)} 
                        onKeyDown={handleKeyDown}/>
                    <input
                        type="number" 
                        placeholder="Max" 
                        className='w-1/2 border-2 border-gray-300 rounded-md p-2' 
                        onChange={(e) => handleFilterChange('potassiumMax', e.target.value)} 
                        onKeyDown={handleKeyDown}/></div>
                </div>
                <div className="w-full">
                    <label className="block text-gray-700 font-bold mt-4 mb-2">Vitamins</label>
                    <div className="flex gap-2"><input
                        type="number" 
                        placeholder="Min" 
                        className='w-1/2 border-2 border-gray-300 rounded-md p-2' 
                        onChange={(e) => handleFilterChange('vitaminsMin', e.target.value)} 
                        onKeyDown={handleKeyDown}/>
                    <input
                        type="number" 
                        placeholder="Max" 
                        className='w-1/2 border-2 border-gray-300 rounded-md p-2' 
                        onChange={(e) => handleFilterChange('vitaminsMax', e.target.value)} 
                        onKeyDown={handleKeyDown}/>
                    </div>
                </div>
                <div className="w-full">
                    <label className="block text-gray-700 font-bold mt-4 mb-2">Manufacturer</label>
                    <input
                    type="text" 
                    placeholder="Manufacturer" 
                    className='w-full border-2 border-gray-300 rounded-md p-2' 
                    onChange={(e) => handleFilterChange('manufacturer', e.target.value)} 
                    onKeyDown={handleKeyDown}/>
                </div>
                <div className="w-full">
                    <label className="block text-gray-700 font-bold mt-4 mb-2">In storage</label>
                    <input
                    type="checkbox"
                    placeholder="Storage" 
                    className='w-1/4 border-gray-300 rounded-md p-2' 
                    onChange={(e) => handleFilterChange('storage', e.target.checked ? true : false)} 
                    onKeyDown={handleKeyDown}/>
                </div>
                <button
                    type="submit"
                    className='select-none bg-blue-500 text-white rounded-md p-2 hover:bg-blue-600 transition-colors duration-250'
                    onClick={applyFilters} // Apply filters
                >
                    Apply Filter
                </button>

            </aside>
            <div id="productCardWrapper" className="w-8/10 flex gap-8 flex-wrap justify-center py-8 px-4 overflow-y-auto h-full">
                {filteredProducts.length === 0 && (
                    <div className='flex flex-col items-center justify-center h-full w-full'>
                        <h1 className='text-2xl font-bold'>No products found</h1>
                        <p className='text-gray-500'>Try adjusting your filters.</p>
                    </div>
                )}
                {filteredProducts.map((product, i) => (
                    <ProductCard key={i} product={product} />
                ))}
            </div>
        </div>
    )
}

export default Storage;