(function () {

    var reactSharp = {

        init: function (ref) {
            ref.$reactSharp = {
                items: [],
            };
        },


        renderJson(ref, json) {
            $reactSharp = ref.$reactSharp;
            if (!json) {
                return;
            }
            var changes = json.c;
            if (!changes) {
                return;
            }

            for (var i = 0, len = changes.length; i < len; i++) {
                var item = changes[i];


                switch (item.c) {
                    case 1: {
                        var element = document.createElement(item.t);
                        $reactSharp.items[item.i] = element;
                        var parentElement = item.p ? $reactSharp.items[item.p] : ref;
                        var a = item.a;
                        if (a) {
                            for (var aName in a) {
                                if (!a.hasOwnProperty(aName)) {
                                    continue;
                                }
                                element.setAttribute(aName, a[aName]);
                            }
                        }
                        parentElement.appendChild(element);
                        break;
                    }
                    case 2: {
                        var element = document.createTextNode(item.v);
                        $reactSharp.items[item.i] = element;
                        var parentElement = item.p ? $reactSharp.items[item.p] : ref;
                        parentElement.appendChild(element);
                        break;
                    }
                    case 3: {
                        var node = $reactSharp.items[item.i];
                        node.remove();                        
                        break;
                    }
                    
                    case 4: {
                        var element = $reactSharp.items[item.i];
                        var a = item.a;
                        if (a) {
                            for (var aName in a) {
                                if (!a.hasOwnProperty(aName)) {
                                    continue;
                                }
                                element.setAttribute(aName, a[aName]);
                            }
                        }                  
                        break;
                    }
                    
                    case 5: {
                        var element = $reactSharp.items[item.i];
                        element.nodeValue = item.v;
                        break;
                    }

                }

            }
        },

        renderJsonString(ref, jsonString) {
            reactSharp.renderJson(ref, JSON.parse(jsonString));
        }

    };
    window.reactSharp = reactSharp;
})();