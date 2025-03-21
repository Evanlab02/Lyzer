# Upgrading to .NET 9

- Author: [Evanlab02](https://github.com/Evanlab02)
- Email: [evanlabuschagne70@gmail.com](mailto:evanlabuschagne70@gmail.com)
- Date: 2025-03-10
- Status: Review
- Planned: No
- Milestone: Not planned

## Summary

We are using .NET 8 in the Lyzer project. We should upgrade to .NET 9 as it has some new features and improvements and to reduce any future headaches.

This is a natural part of development, at the time of creation of the project .NET 8 was the latest version. .NET 9 might have been released but not widely supported yet.

This is not causing any issues asof yet.

## Impact Assessment

- **Severity**: Low
- **Scope**: Component
- **Risk Profile**: Low
- **Development Impact**: No impact, except for actual upgrading work.
- **Area**: Backend

## Proposed Resolution

To upgrade we would need to:

1. Upgrade the .NET version in the project.
    1. Update the .csproj file to use .NET 9
    2. Update our docker images to use .NET 9
    3. Update our GitHub Actions to use .NET 9
    4. Update our documentation to reflect that we are using .NET 9
2. Ensure everything still works as expected
3. Get it merged in.

## Benefits of Resolution

The benefits of upgrading to .NET 9 are:

- New features and improvements that we can use
- Less potential down the line issues

Some potential downsides are:

- We could still be early adopters and have issues with some packages/dependencies
- There is no immediate benefit

## Risks of Deferral

- Future headaches due to:
    - Migration issues if needing to migrate several major versions
    - Potential issues with packages/dependencies
    - Limitations on .NET 8 features

## Acceptance Criteria

- We have upgraded to .NET 9
- Everything is working as expected
- We have updated our documentation to reflect that we are using .NET 9
- We have tested the project with .NET 9
