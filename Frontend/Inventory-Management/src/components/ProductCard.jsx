const ProductCard = ({ product }) => {

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
            <a id="productCard" className="flex flex-col border transition-colors duration-250 border-transparent hover:border-inherit w-80 h-85 rounded-md cursor-pointer">
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
        </>
    )
}

export default ProductCard;