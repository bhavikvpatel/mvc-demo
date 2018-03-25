
!function (e, t) { e.viewloader = t({}, e.jQuery || e.Zepto || e.$) }(this, function (e, t) {
    "use strict";
    var n = function (e) { return e.charAt(0).toUpperCase() + e.slice(1) };
    return e.execute = function (e, r) {
        (r ? r.find("[data-view]") : t("[data-view]")).each(function (r, i) {
            var o = t(i), a = n(o.data("view")); a && e[a] && new e[a](o, i)
        })
    }, e
}),

window.Views = {}

//(function () {
//}).call(this),

!function () {
    var t = function (t, e) { return function () { return t.apply(e, arguments) } };
    Views.FacetTogglePanel = function () {
        function e(e) {
            this.toggleItemsDisplay = t(this.toggleItemsDisplay, this), this.$toggleList = e.prev("ul"),
                this.$items = this.$toggleList.find("li"), this.$toggle = e, this.$toggleLink = e.find(".js-search-facet-toggle__link"),
            e.on("click", ".js-search-facet-toggle__link", this.toggleItemsDisplay)
        } return e.prototype.toggleItemsDisplay = function (t) {
            return t.preventDefault(), this.$items.each(function () {
                if (-1 !== this.className.indexOf("js-collapsible")) return $(this).toggleClass("is-hidden")
            }), this.$toggle.toggleClass("is-expanded"), this.$toggleList.toggleClass("is-expanded"), this.$toggleLink.toggleClass("is-expanded")
        }, e
    }()
}.call(this),


function () {
    var t = function (t, e) { return function () { return t.apply(e, arguments) } }; Views.MultiSelectFacetToggle = function () {
        function e(e) {
            this.$el = e, this.toggleItemSelection = t(this.toggleItemSelection, this),
                this.$el.on("click", ".js-search-facet-multi-select-panel__option", this.toggleItemSelection)
        } return e.prototype.toggleItemSelection = function (t) {
            var e, i; return t.preventDefault(), e = $(t.currentTarget),
                e.toggleClass("is-active"), i = e.find("input[type=checkbox]"), i.prop("checked", !i.prop("checked"))
        }, e
    }()
}.call(this),

function () {
    Views.SearchFacetHorizontalForm = function () {
        function t(t) {
            this.dom = {
                $sortBy: t.find(".js-search-facet-sort-by"),
                $dateAdded: t.find(".js-search-facet-date-added"), $layout: t.find(".js-search-facet-layout-switcher"),
                $sortByOriginal: $(".js-facet-form").find("#sort"), $dateAddedOriginal: $(".js-facet-form").find("#date"), $layoutOriginal: $(".js-facet-form").find("#view")
            },
            this.dom.$sortBy.on("change.searchFacet", function (t) {
                return function (e) {
                    var i;
                    return e.preventDefault(), i = e.currentTarget.options[e.target.selectedIndex].value, t.dom.$sortByOriginal.val(i).change()
                }
            }(this)),
            this.dom.$dateAdded.on("change.searchFacet", function (t) {
                return function (e) {
                    var i; return e.preventDefault(), i = e.currentTarget.options[e.target.selectedIndex].value, t.dom.$dateAddedOriginal.val(i).change()
                }
            }(this)),
            this.dom.$layout.on("click.searchFacet", "a", function (t) {
                return function (e) {
                    var i; return e.preventDefault(), i = e.currentTarget.getAttribute("data-layout-switch"), t.dom.$layoutOriginal.val(i).change()
                }
            }(this))
        } return t
    }()
}.call(this),


function () {
    Views.SearchFacetPanel = function () {
        function t(t) {
            this.dom = {
                $headers: t.find(".js-search-facet-panel__header"),
                $bodies: t.find(".js-search-facet-panel__body"),
                $closeButtons: t.find(".js-search-facet-panel__body-close")
            }, this.dom.$headers.on("click.showFacet", function (t) {
                return function (e) {
                    var i, n, r;
                    return e.preventDefault(),
                        n = $(e.currentTarget), i = t.getFacetBody(n),
                        r = t.getFacetState(i), t.hideAllFacets(),
                        t.showFacet(n, i, r),
                        setTimeout(function () { return t.panelCloseEvents("add") }, 100)
                }
            }(this)),
            this.dom.$closeButtons.on("click.hideAllFacets", function (t) {
                return function (e) {
                    return e.preventDefault(),
                        t.hideAllFacets()
                }
            }(this))
        }
        return t.prototype.getFacetBody = function (t) {
            return t.next(".js-search-facet-panel__body")
        },
            t.prototype.showFacet = function (t, e, i) {
                var n, r, s, o, a, l, c; if ("closed" === i)
                    return r = 300, n = 150,
                        s = e.innerHeight(),
                        l = t.get(0).getBoundingClientRect().bottom, c = $(window).height(), a = c - l, s > r && a < n && e.css("top", "-180px"),
                        t.addClass("is-active"), e.addClass("is-active").removeClass("is-visually-hidden--js"), o = this.getPanelName(t)
            },
            t.prototype.hideAllFacets = function () {
                return this.dom.$headers.removeClass("is-active"),
                    this.dom.$bodies.removeClass("is-active").addClass("is-visually-hidden--js").css("top", ""), this.panelCloseEvents("remove")
            },
            t.prototype.getFacetState = function (t) { return t.hasClass("is-visually-hidden--js") ? "closed" : "open" },
            t.prototype.panelCloseEvents = function (t) {
                return "add" === t ? ($(document).on("keyup.closeFacetPanel",
                    function (t) { return function (e) { var i; if (i = 27, e.keyCode === i) return t.hideAllFacets() } }(this)),
                    $(document).on("click.checkIfOutsidePanel", function (t) {
                        return function (e) {
                            if (!$(e.target).closest(".js-search-facet-panel__body").length)
                                return t.hideAllFacets()
                        }
                    }(this))) : ($(document).off("keyup.closeFacetPanel"),
                    $(document).off("click.checkIfOutsidePanel"))
            },
            t.prototype.getPanelName = function (t) {
                return t.find("h2").data("title")
            }, t
    }()
}.call(this)

