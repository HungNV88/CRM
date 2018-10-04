var DataSourceTree = function(options) {
    this._data = options.data;
    this._delay = options.delay;
};

DataSourceTree.prototype.data = function (options, callback) {
    var self = this;
    var $data = null;

    if (!("name" in options) && !("type" in options)) {
        $data = this._data;//the root tree
        callback({ data: $data });
        return;
    }
    else if ("type" in options && options.type == "folder") {
        if ("additionalParameters" in options && "children" in options.additionalParameters)
            $data = options.additionalParameters.children;
        else $data = {}//no data
    }

    if ($data != null)//this setTimeout is only for mimicking some random delay
        setTimeout(function () { callback({ data: $data }); }, parseInt(Math.random() * 500) + 200);

    //we have used static data here
    //but you can retrieve your data dynamically from a server using ajax call
    //checkout examples/treeview.html and examples/treeview.js for more info
};

var selectTreeFolder = function ($treeEl, folder, $parentEl) {
    var $parentEl = $parentEl || $treeEl;
    if (folder.type == "folder") {
        var $folderEl = $parentEl.find("div.tree-folder-name").filter(function (_, treeFolder) {
            return $(treeFolder).text() == folder.name;
        }).parent();
        $treeEl.one("loaded", function () {
            $.each(folder.children, function (i, item) {
                selectTreeFolder($treeEl, item, $folderEl.parent());
            });
        });
        $treeEl.tree("selectFolder", $folderEl);
    }
    else {
        selectTreeItem($treeEl, folder, $parentEl);
    }
};

var selectTreeItem = function ($treeEl, item, $parentEl) {
    var $parentEl = $parentEl || $treeEl;
    if (item.type == "item") {
        var $itemEl = $parentEl.find("div.tree-item-name").filter(function (_, treeItem) {
            return $(treeItem).text() == item.name && !$(treeItem).parent().is(".tree-selected");
        }).parent();
        $treeEl.tree("selectItem", $itemEl);
    }
    else if (item.type == "folder") {
        selectTreeFolder($treeEl, item, $parentEl);
    }
};

var tree_data = {
    'nhapcontact': { name: 'Nhập contact', type: 'folder', id: 'idnhapcontact' },
    'vehicles': { name: 'Vehicles', type: 'folder', id: 'idvedicles' },
    'rentals': { name: 'Rentals', type: 'folder' },
    'real-estate': { name: 'Real Estate', type: 'folder' },
    'pets': { name: 'Pets', type: 'folder' },
    'tickets': { name: 'Tickets', type: 'item', id: 'idticket' },
    'services': { name: 'Services', type: 'item' },
    'personals': { name: 'Personals', type: 'item' }
}
tree_data['for-sale']['additionalParameters'] = {
    'children': {
        'appliances': { name: 'Appliances', type: 'item' },
        'arts-crafts': { name: 'Arts & Crafts', type: 'item' },
        'clothing': { name: 'Clothing', type: 'item' },
        'computers': { name: 'Computers', type: 'item' },
        'jewelry': { name: 'Jewelry', type: 'item' },
        'office-business': { name: 'Office & Business', type: 'item' },
        'sports-fitness': { name: 'Sports & Fitness', type: 'item' }
    }
}
tree_data['vehicles']['additionalParameters'] = {
    'children': {
        'cars': { name: 'Cars', type: 'folder' },
        'motorcycles': { name: 'Motorcycles', type: 'item' },
        'boats': { name: 'Boats', type: 'item' }
    }
}
tree_data['vehicles']['additionalParameters']['children']['cars']['additionalParameters'] = {
    'children': {
        'classics': { name: 'Classics', type: 'item' },
        'convertibles': { name: 'Convertibles', type: 'item' },
        'coupes': { name: 'Coupes', type: 'item' },
        'hatchbacks': { name: 'Hatchbacks', type: 'item' },
        'hybrids': { name: 'Hybrids', type: 'item' },
        'suvs': { name: 'SUVs', type: 'item' },
        'sedans': { name: 'Sedans', type: 'item' },
        'trucks': { name: 'Trucks', type: 'item' }
    }
}

tree_data['rentals']['additionalParameters'] = {
    'children': {
        'apartments-rentals': { name: 'Apartments', type: 'item' },
        'office-space-rentals': { name: 'Office Space', type: 'item' },
        'vacation-rentals': { name: 'Vacation Rentals', type: 'item' }
    }
}
tree_data['real-estate']['additionalParameters'] = {
    'children': {
        'apartments': { name: 'Apartments', type: 'item' },
        'villas': { name: 'Villas', type: 'item' },
        'plots': { name: 'Plots', type: 'item' }
    }
}
tree_data['pets']['additionalParameters'] = {
    'children': {
        'cats': { name: 'Cats', type: 'item' },
        'dogs': { name: 'Dogs', type: 'item' },
        'horses': { name: 'Horses', type: 'item' },
        'reptiles': { name: 'Reptiles', type: 'item' }
    }
}

var treeDataSource = new DataSourceTree({ data: tree_data });