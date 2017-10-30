var devtodevPlugin = {
    /**
     * Check IndexedDB available
     * @return bool
     */
    IsIndexedDBAvailable: function() {
        var isAvailable = false;
        window.indexedDB = window.indexedDB ||
            window.mozIndexedDB ||
            window.webkitIndexedDB ||
            window.msIndexedDB;

        if (window.indexedDB) isAvailable = true;

        return isAvailable;
    },

    /**
     * @param key string
     * @param value string
     * @return void
     */
    SetItem: function(key, value) {
        window.localStorage.setItem(Pointer_stringify(key), Pointer_stringify(value));
    },

    /**
     * @param key string
     * @return object || null
     */
    GetItem: function(key) {
        var result = window.localStorage.getItem(Pointer_stringify(key));
        result = result === 'undefined' ? null : result;
        if (result != null) {
            var buffer = _malloc(lengthBytesUTF8(result) + 1);
            writeStringToMemory(result, buffer);
            return buffer;
        }
        return null;
    },

    /**
     * @param key string
     * @return bool
     */
    IsExistItem: function(key) {
        var result = window.localStorage.getItem(key);
        result = result === 'undefined' ? false : true;
        return result;
    },

    /**
     * @param key string
     * @return void
     */
    RemoveItem: function(key) {
        window.localStorage.removeItem(key);
    }
};

mergeInto(LibraryManager.library, devtodevPlugin);