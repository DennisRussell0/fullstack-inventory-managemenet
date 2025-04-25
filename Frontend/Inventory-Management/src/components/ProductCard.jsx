import { useState } from "react";

const ProductCard = ({ product }) => {
    const [modalOpen, setModalOpen] = useState(false);

    if(product.ImagePath !== null){
        var mime = product.ImagePath.split('.').pop().toLowerCase();
        if (mime == "jpg"){
            mime = "jpeg";
        }
    }
    else{
        var mime = "none";
    }
    return (
        <>
            <a id="productCard" className="flex flex-col border transition-colors duration-250 border-transparent hover:border-inherit w-80 h-85 rounded-md cursor-pointer" onClick={() => {setModalOpen(prev => !prev)}}>
                <img className="flex place-self-center min-h-50 w-50 object-contain w-full bg-white p-4 rounded-t-md" src={`data:image/${mime};base64,${product.Image}`} alt={product.Name}/>
                <div className="flex h-full justify-between m-4">
                    <div className="flex flex-col h-full w-[50%] gap-2">
                        <p className="font-semibold">{product.Manufacturer}</p>
                        <h3 className="text-xl font-bold text-left line-clamp-2">{product.Name}</h3>
                    </div>
                    <div className="flex w-[45%] flex-col gap-4 items-end">
                        <div className="flex gap-2">
                            <p className="font-light">Stock:</p>
                            <p className="font-semibold">{product.Storage}</p>
                        </div>
                    </div>
                </div>
            </a>
            {modalOpen && (
                <div
    className="fixed inset-0 flex items-center justify-center backdrop-blur-sm z-50"
    onClick={() => setModalOpen(false)} // Close modal when clicking outside
>
    <div
        className="bg-white p-8 rounded-lg shadow-2xl w-[90%] max-w-4xl border flex flex-col md:flex-row gap-8"
        onClick={(e) => e.stopPropagation()} // Prevent click event from propagating to the parent
    >
        {/* Left Column: Image */}
        <div className="flex-shrink-0 w-full md:w-1/3">
            <img
                className="w-full h-auto object-cover rounded-md shadow-md"
                src={`data:image/${mime};base64,${product.Image}`}
                alt={product.Name}
            />
        </div>

        {/* Right Column: Product Details */}
        <div className="flex-grow">
            <h2 className="text-3xl font-bold mb-6 text-gray-800">{product.Name}</h2>
            <div className="grid grid-cols-2 gap-4 text-gray-700">
                <p><strong>Manufacturer:</strong> {product.Manufacturer}</p>
                <p><strong>Stock:</strong> {product.Storage}</p>
                <p><strong>Price:</strong> {product.Price} kr.</p>
                <p><strong>Type:</strong> {product.Type}</p>
                <p><strong>Calories:</strong> {product.Calories}</p>
                <p><strong>Protein:</strong> {product.Protein}</p>
                <p><strong>Fat:</strong> {product.Fat}</p>
                <p><strong>Sodium:</strong> {product.Sodium}</p>
                <p><strong>Fiber:</strong> {product.Fiber}</p>
                <p><strong>Carbs:</strong> {product.Carbs}</p>
                <p><strong>Sugars:</strong> {product.Sugars}</p>
                <p><strong>Potassium:</strong> {product.Potassium}</p>
                <p><strong>Vitamins:</strong> {product.Vitamins}</p>
                <p><strong>Shelf:</strong> {product.Shelf}</p>
                <p><strong>Weight:</strong> {product.Weight}</p>
                <p><strong>Cups:</strong> {product.Cups}</p>
                <p><strong>Rating:</strong> {product.Rating}</p>
            </div>
        </div>
    </div>
</div>
            )}
        </>
    )
}

export default ProductCard;