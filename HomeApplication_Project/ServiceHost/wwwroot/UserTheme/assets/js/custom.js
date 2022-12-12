const cookieName = "cart-items";
const hostName = location.protocol + '//' + location.host;

function addToCart(id, name, price, picture) {
    let products = $.cookie(cookieName);
    if (products === undefined) {
        products = [];
    } else {
        products = JSON.parse(products);
    }

    const count = $("#productCount").val();
    const currentProduct = products.find(x => x.id === id);
    if (currentProduct !== undefined) {
        products.find(x => x.id === id).count = parseInt(currentProduct.count) + parseInt(count);
    } else {
        const product = {
            id,
            name,
            unitPrice: price,
            picture,
            count
        }

        products.push(product);
    }

    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    updateCart();
}

function updateCart() {
    //debugger;
    let products = $.cookie(cookieName);
    products = JSON.parse(products);
    $("#cart_items_count,#cart_items_count_2").each(function ()
    {
        let count = 0
        products.forEach((item) => {
           // debugger
            count += Number(item.count)
        })
        $(this).text(count);

    });
    const cartItemsWrapper = $("#cart_items_wrapper");
    //debugger;
    cartItemsWrapper.html('');
    products.forEach(x => {

        const product = `<div class="single-cart-item">
                            <a href="javascript:void(0)" class="remove-icon" onclick="removeFromCart('${x.id}')">
                                <i class="ion-android-close"></i>
                            </a>
                            <div class="image">
                                <a href="/Cart/?id=${x.id}&handler=RedirectToProductPage" >
                                    <img src="/ProductPictures/${x.picture}"
                                         class="img-fluid" alt="">
                                </a>
                            </div>
                            <div class="content">
                                <p class="product-title">
                                    <a href="/Cart/?id=${x.id}&handler=RedirectToProductPage">محصول: ${x.name}</a>
                                </p>
                                <p class="count">تعداد: ${x.count}</p>
                                <p class="count">قیمت واحد: ${x.unitPrice}</p>
                            </div>
                        </div>`;

        cartItemsWrapper.append(product);
    });
}

function removeFromCart(id) {
    debugger
    if (location.pathname.includes('/Checkout') ) {

        alert("امکان تغیر سبد خرید در این مرحله وجود ندارد!")
        return false;
    }
    else {

        let products = $.cookie(cookieName);
        products = JSON.parse(products);
        const itemToRemove = products.findIndex(x => x.id === id);
        const itemToRemoveName = products[itemToRemove].name;
        if (confirm(`آیا مطمئن به حذف کالای ${itemToRemoveName} از سبد خرید خود هستید؟`)) {
            products.splice(itemToRemove, 1);
            $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
            updateCart();
        }
    }
}

function changeCartItemCount(id, totalId, count) {
    //debugger;
    var products = $.cookie(cookieName);
    products = JSON.parse(products);
    const productIndex = products.findIndex(x => x.id == id);
    products[productIndex].count = count;
    const product = products[productIndex];
    const newPrice = parseFloat(product.unitPrice) * parseFloat(count);

    PriceFormatter(newPrice, totalId);

    //$(`#${totalId}`).text(newPrice);

    

    //products[productIndex].totalPrice = newPrice;
   // debugger;
    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    updateCart();

    const settings = {
        "url": hostName + "/api/Inventory",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({ "productId": id, "count": count })
    };

    $.ajax(settings).done(function (data) {
        if (data.isStock == false) {
            const warningsDiv = $('#productStockWarnings');
            if ($(`#${id}`).length == 0) {
                warningsDiv.append(`
                    <div class="alert alert-warning" id="${id}">
                        <i class="fa fa-warning"></i> کالای
                        <strong>${data.productName}</strong>
                        در انبار کمتر از تعداد درخواستی موجود است.
                    </div>
                `);
            }
        } else {
            if ($(`#${id}`).length > 0) {
                $(`#${id}`).remove();
            }
        }
    });
}

function PriceFormatter(price, totalId) {

   // debugger
    var callsettings = {
        "url": hostName + "/api/Inventory/PriceFormatter/" + price,
        "method": "GET",
        "timeout": 0,
    };
     debugger
    $.ajax(callsettings).done(function (response) {

        let result = response;

        $(`#${totalId}`).text(result);

//        console.log(response);
    });

    
}