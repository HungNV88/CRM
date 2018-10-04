(function ($) {
    /*
     * This is a plugin for jqgrid to enable rowspan inside it.
     *	This plugin is developed by Prashant Rana
     */

    $.jgrid.extend({
        applyRowSpan: function (p) {
            function callRowSpanCol(p, rsCOl, col) {
                var counts = 1;
                var tempt = 0;
                $.each($("td[aria-describedby$=" + col.name + "]"), function () {

                    var currTarget = this;
                    var index = parseInt(currTarget.parentNode.id);
                    //var value = eval("p.data[" + (index) + "]." + rsCOl);
                    var value = $("#grid-table-lichhen").jqGrid('getCell', index, 'KhungGioId');
                    if (counts == 1) { tempt = value; next = 0; }
                    if (counts == 1) {
                        target = { el: currTarget, count: 1 };
                    } else if (value != tempt) {
                        target = {};
                        target = { el: currTarget, count: 1 };
                        tempt = value;

                    } else {
                        target.count++;
                        $(currTarget).css("display", "none");
                        $(target.el).attr("rowspan", target.count);
                    }
                    counts++;
                });
            }
            function callRowSpan(p, rsCol) {
                $.each(p.colModel, function () {
                    if (this.rowSpanIndex != null && this.rowSpanIndex === rsCol) {
                        callRowSpanCol(p, rsCol, this);
                    }
                });
            }
            return this.each(function () {
                var $me = this;
                if (!$me.grid) { return; }
                if (!$me.p) { return; }
                var $p = $me.p;
                if ($p.rowSpan && $p.rowSpanCol && $p.rowSpanCol.length > 0) {
                    $.each($p.rowSpanCol, function () {
                        callRowSpan($p, this.toString());
                    });
                } else { return; }
            });
        }
    });
})(jQuery);
