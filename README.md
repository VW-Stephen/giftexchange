# giftexchange

A simple gift exchange manager, allows you to input participants and randomly assign them to other participants.

## Tech

**Backend:** Written in C# using .NET Core 2.0 and EF Code First models.

**Frontend:** AngularJS

## Known Issues/Improvements

- AngularJS doesn't like running the function to apply the mapping from participant ID to name after `$digest.$apply()` is called,
refactoring that into a direct mapping (store an array in the `$scope` that maps those values directly) would work better.
- Clicking save on each new participant added is lame, tracking object changes in the frontend and automatically calling the `save()`
function would be cooler.
- Data validation is lax, so you can add people/exchanges without valid data if you feel like it.
- Email is stubbed out but not actually tied to an email provider on the backend.
