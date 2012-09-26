/* ===================================================
 * VeloTours.js v2.1.1
 * http://
 * ===================================================
 * Copyright 2012 Henrik Næss
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * ========================================================== */

$(function () {


    $('.athleteLink').each(function () {
        var links = '<a target="_blank" data-parentid="athleteid' + $(this).data("athleteid") + '" class="popoverLink" href="http://www.strava.com/athletes/' + $(this).data("athleteid") + '" title="View athlete on Strava (external link)">View on Strava</a><br/>';
        links += '<a target="_blank" data-parentid="athleteid' + $(this).data("athleteid") + '" class="popoverLink" href="http://veloviewer.com/AthleteSegments.php?athleteId=' + $(this).data("athleteid") + '" title="View as all athletes segments on VeloViewer (external link)">View on VeloViewer</a><br/>';

        $(this).popover({ title: 'Athlete links <button type="button" data-parentid="athlete' + $(this).data("athleteid") + '" class="close popoverClose" data-dismiss="alert">x</button>', content: links, trigger: 'manual' });
    });

    $('.athleteLink').click(function () {
        $(this).popover('toggle');

        $('.popoverClose').click(function () {
            $('#' + $(this).data('parentid')).popover('hide');
        });

        $('.popoverLink').click(function () {
            $('#' + $(this).data('parentid')).popover('hide');
        });
        return false;
    });

})