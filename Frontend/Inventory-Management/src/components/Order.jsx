import { useState } from "react";

const Order = ({ order }) => {
    const [collapsed, setCollapsed] = useState(true);

    return (
        <div
            onClick={() => setCollapsed(!collapsed)} // Toggle collapsed state on click
        >
            {/* Collapsed View */}
            {collapsed ? (
                <div className="grid grid-cols-4 text-center border-b p-4 font-semibold cursor-pointer">
                    <p>#{order.id}</p>
                    <p>{order.buyer}</p>
                    <p>{new Date(order.date).toLocaleDateString()}</p>
                    <p>{order.price} DKK</p>
                </div>
            ) : (
                // Expanded View
                <div className="text-center border-b font-semibold cursor-pointer">
                    <div className="grid grid-cols-4 text-center p-4">
                        <p>#{order.id}</p>
                        <p>{order.buyer}</p>
                        <p>{new Date(order.date).toLocaleDateString()}</p>
                        <p>{order.price} DKK</p>
                    </div>
                    <div className="flex flex-col gap-6 p-4 text-start font-semibold">
                        <p>Address: {order.address}</p>
                        <div>
                            <p>Products:</p>
                            <ul className="pl-8 pt-2 font-normal">
                                {order.products.map((product, index) => (
                                    <li key={index}>
                                        #{product.productId}, Quantity: {product.quantity}
                                    </li>
                                ))}
                            </ul>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
};

export default Order;